using APIHandler;
using APIHandler.SequoiaApiScheme;
using CsvHelper.Configuration;
using Newtonsoft.Json;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Olive;
using Olive.Entities;
using Olive.Export;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Domain
{
    public interface IEADShipmentService
    {
        Task Transmit(Consignment consignment, bool shouldTransmit = false, bool isStatusUpdate = true);
        Task Transmit();
        Task<Consignment> CheckRouteStatus(Consignment consignment, string code = null);
        Task<Consignment> CheckRouteOnly(Consignment consignment, string code = null);
        Task SendControlDeclaration(Consignment consignment);
        Task CancelDeclaration(Consignment consignment, bool isArchive = false);
        Task ShipmentFileDeliveries();
        Task TransmitGVMS();
        Task TransmitINV();
        Task UpdateInToUKType();
        Task ArchiveWithImporter();
        Task DeleteNotification(Shipment shipment);
    }

    public class EADShipmentService : IEADShipmentService
    {
        IDatabase Database;
        IASMMapper ASMMapper;
        IASMService ASMService;
        static SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        static readonly int GVMSTime = 24;

        public EADShipmentService(IDatabase database, IASMService asmService)
        {
            Database = database;
            ASMMapper = Context.Current.GetService<IASMMapper>();
            ASMService = asmService;
        }

        Expression<Func<Consignment, bool>> Trasnmitable => t => (t.ProgressId.IsAnyOf(Helper.Transmitables))
                                                             && !t.Shipment.IsDeactivated;

        async Task<IEnumerable<Consignment>> GetReadyToTransmitConsignment()
        {
            var query = Database.Of<Consignment>().Where(Trasnmitable);
            return await query.GetList();
        }

        public async Task Transmit(Consignment consignment, bool shouldTransmit = false, bool isStatusUpdate = true)
        {
            try
            {
                if (consignment.Shipment.IsInUK)
                    await TransmitIntoUK(consignment, shouldTransmit, isStatusUpdate);
                else
                    await TransmitOutUK(consignment, isStatusUpdate);
            }
            catch (Exception ex)
            {
                await UpdateTransitRetires(consignment);
                Log.For<Consignment>().Info(ex.Message);
                throw;
            }
        }

        public async Task Transmit()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                var list = await GetReadyToTransmitConsignment();
                await list.DoAsync(async (item, row) =>
                {
                    try
                    {
                        await Transmit(item, shouldTransmit: false);
                    }
                    catch (ValidationException ex)
                    {
                        if (ex.Message != "This shipment is not due.")
                        {
                            await UpdateTransitRetires(item);
                            Log.For(this).Error(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        await UpdateTransitRetires(item);
                        Log.For(this).Error(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Log.For(this).Error(ex);
                throw new ValidationException("There is an error in calling the API, please look into the logs.");
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        public async Task<Consignment> CheckRouteStatus(Consignment consignment, string code = null)
        {
            var declaration = ASMMapper.MapFrontierReportDeclarationRequest(consignment, code);

            var request = declaration.ToXmlString();

            try
            {

                await consignment.Log(request, LogType.GetCustomsReportDeclarationRequest);

                var result = await ASMService.GetFrontierDeclarationResponse(request);

                await consignment.Log(result.ToXmlString(cleanUp: false), LogType.GetCustomsReportDeclarationResponse);

                if (result.GetFrontierDeclarationResponseResult.Errors.HasAny())
                    await consignment.FlagAsRejected(string.Join(",", result.GetFrontierDeclarationResponseResult.Errors.Select(x => x.ErrorMessage)));
                else
                    await UpdateRout(consignment, result.GetFrontierDeclarationResponseResult.ReturnValue);

                return await Database.Reload(consignment);
            }
            catch (Exception ex)
            {
                await consignment.FlagAsInternalError(ex.Message);
                throw;
            }
        }

        public async Task<Consignment> CheckRouteOnly(Consignment consignment, string code = null)
        {
            var declaration = ASMMapper.MapFrontierReportDeclarationRequest(consignment, code);
            var request = declaration.ToXmlString();

            try
            {

                var result = await ASMService.GetFrontierDeclarationResponse(request);

                if (result.GetFrontierDeclarationResponseResult.Errors.None())
                    await UpdateRoute(consignment, result.GetFrontierDeclarationResponseResult.ReturnValue);

                return await Database.Reload(consignment);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task TransmitIntoUK(Consignment consignment, bool shouldTransmit = false, bool isStatusUpdate = true)
        {
            try
            {
                if (!shouldTransmit && consignment.IntoUKTypeId == PortType.GVMS)
                {
                    if (consignment.Shipment.Route?.UKPort?.IntoUKValue != "D" && consignment.Shipment.ExpectedDate.Date > LocalTime.Today.Date)
                        throw new ValidationException("This shipment is not due.");

                    if (consignment.Shipment.Route?.UKPort?.IntoUKValue == "D" && consignment.Shipment.ExpectedDate < new DateTime(2022, 01, 01) && !consignment.Shipment.IsInUK)
                        throw new ValidationException("This shipment is not due.");
                }

                if (consignment.UCN.IsEmpty() && consignment.Shipment.IsInUK && consignment.IntoUKTypeId == PortType.Inventory)
                    throw new ValidationException("This consignment doesn't have UCN Number.");

                var declaration = await ASMMapper.Map(consignment);

                var request = declaration.ToXmlString();

                if (consignment.NeedToSendAmendment)
                    await AmendIntoUKShipment(consignment, request, isStatusUpdate);
                else
                    await CreateIntoUkShipment(consignment, request, isStatusUpdate);
            }
            catch (ValidationException ex)
            {
                throw ex.InnerException ?? ex;
            }
            catch (Exception ex)
            {
                await consignment.FlagAsInternalError(ex.Message);
                throw ex?.InnerException ?? ex;
            }

        }

        async Task TransmitOutUK(Consignment consignment, bool isStatusUpdate = true)
        {
            try
            {
                var declaration = await ASMMapper.MapExportEAD(consignment);

                var request = declaration.ToXmlString();

                if (consignment.NeedToSendAmendment)
                    await AmendOutOfUkShipment(consignment, request, isStatusUpdate);
                else
                    await CreateOutOfUkShipment(consignment, request, isStatusUpdate);
            }
            catch (Exception ex)
            {
                await consignment.FlagAsInternalError(ex.Message);
                throw;
            }
        }

        private async Task SendDeclaration(Consignment consignment, bool isStatusUpdate = true)
        {
            var sendObject = new AsmSendGbDeclaration { DeclarationIdentity = new AsmDeclarationIdentity { DeclarationUcr = consignment.UCR } };
            var sendRequest = sendObject.ToXmlString();
            await consignment.Log(sendRequest, LogType.SendASMDeclarationRequest);
            var sendResult = await ASMService.SendDeclaration(sendRequest);

            await consignment.Log(sendResult.ToXmlString(cleanUp: false), LogType.SendASMDeclarationResponse);

            if (sendResult.SendGbDeclarationResult.Errors.None() && isStatusUpdate)
                await consignment.FlagAsAccepted();
            else if (isStatusUpdate)
                await consignment.FlagAsRejected(string.Join(",", sendResult.SendGbDeclarationResult.Errors.Select(x => x.ErrorMessage)));
        }

        public async Task CancelDeclaration(Consignment consignment, bool isArchive = false)
        {
            if (consignment.Shipment.IsInUK)
            {
                var port = consignment.Shipment.Route?.UKPort;
                var declaration = await ASMMapper.Map(consignment);
                if (isArchive)
                {
                    declaration.ReasonForAmendment = "Archived the consignment";
                }
                else
                {
                    if (await consignment.EntryTypeText(port) == "GVMS")
                        declaration.ReasonForAmendment = "Changing from GVMS to Inventory";
                    else
                        declaration.ReasonForAmendment = "Changing from Inventory to GVMS";
                }
                var request = declaration.ToXmlString();
                await AmendWithOutDeclarationIntoUKShipment(consignment, request);
            }
            var sendObject = new AsmSendGbDeclaration { DeclarationIdentity = new AsmDeclarationIdentity { DeclarationUcr = consignment.UCR } };
            var sendRequest = sendObject.ToXmlString();
            await consignment.Log(sendRequest, LogType.CancelASMDeclarationRequest);
            var sendResult = await ASMService.CancelDeclaration(sendRequest);
            await consignment.Log(sendResult.ToXmlString(cleanUp: false), LogType.CancelASMDeclarationResponse);
        }

        private async Task CreateIntoUkShipment(Consignment consignment, string request, bool isStatusUpdate = true)
        {
            await consignment.Log(request, LogType.CreateASMDeclarationRequest);

            var result = await ASMService.CreateShipmentIntoUK(request);

            await consignment.Log(result.ToXmlString(cleanUp: false), LogType.CreateASMDeclarationResponse);

            if (result.CreateImportGbDeclarationResult.Errors.HasAny() && isStatusUpdate)
            {
                await consignment.FlagAsRejected(string.Join(",", result.CreateImportGbDeclarationResult.Errors.Select(x => x.ErrorMessage)));
                return;
            }

            if (consignment.CanSendToCustom && isStatusUpdate)
            {
                if (await consignment.HasControl(consignment))
                {
                    await EmailTemplate.SendControlEmail(consignment.Shipment.TrackingNumber, consignment.Shipment.MyReferenceForCPInvoice, consignment.UKTrader.Name, consignment.ConsignmentNumber);
                    await consignment.FlagAsHMRCControlled();
                }
                else
                {
                    await SendDeclaration(consignment);
                }
            }
            else if (consignment.ProgressId == Progress.ManualQuota)
            {
                await EmailTemplate.SendManualQuota(consignment.Shipment.TrackingNumber, consignment.Shipment.MyReferenceForCPInvoice, consignment.UKTrader.Name, consignment.ConsignmentNumber);
                await consignment.FlagAsAccepted();
            }
            else if (isStatusUpdate)
                await consignment.FlagAsAccepted();
        }

        private async Task AmendIntoUKShipment(Consignment consignment, string request, bool isStatusUpdate = true)
        {
            await consignment.Log(request, LogType.AmendASMDeclarationRequest);

            var result = await ASMService.AmendShipmentIntoUK(request);

            await consignment.Log(result.ToXmlString(cleanUp: false), LogType.AmendASMDeclarationResponse);

            if (result.AmendAcceptedImportGbDeclarationResult.Errors.HasAny() && isStatusUpdate)
            {
                await consignment.FlagAsRejected(string.Join(",", result.AmendAcceptedImportGbDeclarationResult.Errors.Select(x => x.ErrorMessage)));
                return;
            }

            await SendDeclaration(consignment, isStatusUpdate);
        }
        private async Task AmendWithOutDeclarationIntoUKShipment(Consignment consignment, string request, bool isStatusUpdate = true)
        {
            await consignment.Log(request, LogType.AmendASMDeclarationRequest);
            var result = await ASMService.AmendShipmentIntoUK(request);
            await consignment.Log(result.ToXmlString(cleanUp: false), LogType.AmendASMDeclarationResponse);
            if (result.AmendAcceptedImportGbDeclarationResult.Errors.HasAny() && isStatusUpdate)
            {
                await consignment.FlagAsRejected(string.Join(",", result.AmendAcceptedImportGbDeclarationResult.Errors.Select(x => x.ErrorMessage)));
                return;
            }
        }

        private async Task CreateOutOfUkShipment(Consignment consignment, string request, bool isStatusUpdate = true)
        {
            await consignment.Log(request, LogType.CreateASMDeclarationRequest);

            var result = await ASMService.CreateShipmentOutOfUK(request);

            await consignment.Log(result.ToXmlString(cleanUp: false), LogType.CreateASMDeclarationResponse);

            if (result.CreateExportGbDeclarationResult.Errors.HasAny() && isStatusUpdate)
            {
                await consignment.FlagAsRejected(result.CreateExportGbDeclarationResult.ToString());
                return;
            }

            if (consignment.CanSendToCustom && isStatusUpdate)
            {
                if (await consignment.HasControl(consignment))
                {
                    await EmailTemplate.SendControlEmail(consignment.Shipment.TrackingNumber, consignment.Shipment.MyReferenceForCPInvoice, consignment.UKTrader.Name, consignment.ConsignmentNumber);
                    await consignment.FlagAsHMRCControlled();
                }
                else
                {
                    await SendDeclaration(consignment, isStatusUpdate);
                }
            }
            else if (isStatusUpdate)
                await consignment.FlagAsAccepted();
        }

        private async Task AmendOutOfUkShipment(Consignment consignment, string request, bool isStatusUpdate = true)
        {
            await consignment.Log(request, LogType.AmendASMDeclarationRequest);

            var result = await ASMService.AmendShipmentOutOfUK(request);
            await consignment.Log(result.ToXmlString(cleanUp: false), LogType.AmendASMDeclarationResponse);

            if (result.AmendAcceptedExportGbDeclarationResult.Errors.HasAny() && isStatusUpdate)
            {
                await consignment.FlagAsRejected(string.Join(",", result.AmendAcceptedExportGbDeclarationResult.Errors.Select(x => x.ErrorMessage)));
                return;
            }

            await SendDeclaration(consignment, isStatusUpdate);
        }

        private async Task UpdateRout(Consignment consignment, string response)
        {
            var xml = new XmlDocument();
            xml.LoadXml(response);
            try
            {
                var route = xml.ChildNodes[1].ChildNodes[2].InnerText.TrimOrEmpty();
                var epu = xml.ChildNodes[1].ChildNodes[1].InnerText.TrimOrEmpty();

                if (route.IsAnyOf("6", "3"))
                {
                    await consignment.FlagAsArrived(route, epu);
                }
                else
                {
                    await consignment.FlagAsWithCustoms(route, epu);
                    await EmailTemplate.CustomsIntervention.Send(consignment.Shipment.PrimaryContact.Email,
                    new
                    {
                        TRACKINGNUMBER = consignment.Shipment.TrackingNumber,
                        ROUTENUMBER = route,
                        CUSTOMERREFERENCE = consignment.Shipment.MyReferenceForCPInvoice
                    }, bccs: new List<string> { AppSetting.CustomsProEmail });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateRoute(Consignment consignment, string response)
        {
            var xml = new XmlDocument();
            xml.LoadXml(response);
            try
            {
                var route = xml.ChildNodes[1].ChildNodes[2].InnerText.TrimOrEmpty();
                var epu = xml.ChildNodes[1].ChildNodes[1].InnerText.TrimOrEmpty();
                await consignment.UpdateEntryReference(route, epu);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateTransitRetires(Consignment consignment)
        {
            var reload = await Database.Reload(consignment);
            var endDate = consignment.Shipment.ExpectedDate;
            if (endDate.Date != DateTime.MaxValue.Date)
                endDate = endDate.AddDays(2);

            if (endDate.Date == DateTime.MaxValue.Date || endDate <= LocalTime.Now) // adding leverage for validations for any client to for completing the shipment
            {
                const int maxTry = 11;
                var retries = reload.TransmitRetries + 1;
                if (reload.TransmitRetries > 10)
                {
                    await reload.FlagAsDraft(retries);
                    if (reload.TransmitRetries == maxTry)
                    {
                        await EmailTemplate.ErrorWhileTransitASM.Send(AppSetting.CustomsProEmail, new
                        {
                            TRACKINGNUMBER = reload.ConsignmentNumber,
                            CUSTOMERREFERENCE = reload.Shipment.MyReferenceForCPInvoice
                        });
                    }
                }
                else
                {
                    await Database.Update(reload, x => x.TransmitRetries = retries, SaveBehaviour.BypassAll);
                }
            }
        }
        public async Task SendControlDeclaration(Consignment consignment)
        {
            await SendDeclaration(consignment);
        }
        //amendent when edit vehicle number 
        Expression<Func<Consignment, bool>> TrasnmitableCondition => t => t.ProgressId.IsAnyOf(Helper.Transmit)
                                                            && !t.Shipment.IsDeactivated;


        public async Task ShipmentFileDeliveries()
        {
            var consigments = await Database.Of<Consignment>()
                .Where(x => !x.HasShipmentFileDelivery && x.ICSMRNNumber.HasValue() && x.EntryReference.HasValue())
                .GetList()
                .ToList();

            await consigments.DoAsync(async (item, row) => await EmailTemplate.ShipmentFileDeliveries(item));

        }

        //trasmit gvms and inv isd and sfd message
        Expression<Func<Consignment, bool>> TrasnmitableGMVS => x => x.IntoUKTypeId == PortType.GVMS
                            && x.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && x.UKTrader.UsingEIDR == true
                            && x.Shipment.ExpectedDate < LocalTime.Now.AddHours(-GVMSTime)
                                                            && !x.Shipment.IsDeactivated;

        Expression<Func<Consignment, bool>> TrasnmitableSFD => x => x.IntoUKTypeId == PortType.Inventory && x.ProgressId == Progress.Cleared
                           && ((x.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && x.UKTrader.UsingEIDR == true) ||
                              (x.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Own) && x.UKTrader.SFDOnly == false))
                                                           && !x.Shipment.IsDeactivated;
        public async Task TransmitGVMS()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                var list = await Database.Of<Consignment>().Where(TrasnmitableGMVS).GetList().ToList();
                await list.DoAsync(async (item, row) =>
                {
                    try
                    {
                        await Transmit(item, shouldTransmit: false);
                    }
                    catch (ValidationException ex)
                    {
                        if (ex.Message != "This shipment is not due.")
                        {
                            await UpdateTransitRetires(item);
                            Log.For(this).Error(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        await UpdateTransitRetires(item);
                        Log.For(this).Error(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Log.For(this).Error(ex);
                throw new ValidationException("There is an error in calling the API, please look into the logs.");
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        public async Task TransmitINV()
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                var list = await Database.Of<Consignment>().Where(TrasnmitableSFD).GetList().ToList();
                await list.DoAsync(async (item, row) =>
                {
                    try
                    {
                        await Transmit(item, shouldTransmit: false);
                    }
                    catch (ValidationException ex)
                    {
                        if (ex.Message != "This shipment is not due.")
                        {
                            await UpdateTransitRetires(item);
                            Log.For(this).Error(ex);
                        }
                    }
                    catch (Exception ex)
                    {
                        await UpdateTransitRetires(item);
                        Log.For(this).Error(ex);
                    }
                });
            }
            catch (Exception ex)
            {
                Log.For(this).Error(ex);
                throw new ValidationException("There is an error in calling the API, please look into the logs.");
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }

        //run for firt time only for old data to updated.
        public async Task UpdateInToUKType()
        {
            var list = await Database.Of<Consignment>().Where(x => x.Shipment.TypeId == ShipmentType.IntoUk && x.IntoUKTypeId == null).GetList();

            await list.DoAsync(async (item, row) =>
            {
                try
                {
                    var portType = await item.ConsigmentsPort(item.Shipment.Route?.UKPort);
                    if (portType != null)
                        await Database.Update(item, x => x.IntoUKType = portType, SaveBehaviour.BypassAll);
                }
                catch
                {
                    throw;
                }
            });

        }

        private async Task<Blob> GenerateCFSPExcelFile(IEnumerable<Consignment> consignments)
        {

            using (var stream = new MemoryStream())
            {
                var xlPackage = new ExcelPackage(stream);
                // Define a worksheet
                var worksheet = xlPackage.Workbook.Worksheets.Add("CFSP-" + LocalTime.Now.ToString("dd/MM/yyyy"));

                worksheet.DefaultColWidth = 18;
                // First row
                var startRow = 3;
                var row = startRow;

                worksheet.Cells["H1"].Value = "CFSP Customs Freight Simplified Procedure - Record Keeping & FSD Reporting";
                using (var r = worksheet.Cells["H1:O1"])
                {
                    r.Merge = true;
                    r.Style.Font.Bold = true;
                    r.Style.Font.Size = 18;
                }

                worksheet.Cells["A2"].Value = "Unique Consignment Reference No";
                worksheet.Cells["B2"].Value = "Vehicle No";
                worksheet.Cells["C2"].Value = "Trailer No";

                worksheet.Cells["D2"].Value = "SFD Frontier EPU";
                worksheet.Cells["E2"].Value = "SFD Frontier Declaration Number";
                worksheet.Cells["F2"].Value = "SFD Frontier Declaration Date";

                worksheet.Cells["G2"].Value = "EIDR Date & Time of Receipt of goods into Storage";
                worksheet.Cells["H2"].Value = "Supplier";
                worksheet.Cells["I2"].Value = "Importers details";

                worksheet.Cells["J2"].Value = "Tracking No.";
                worksheet.Cells["K2"].Value = "Total Packages";
                worksheet.Cells["L2"].Value = "Invoice Number";

                worksheet.Cells["M2"].Value = "Commodity Code";
                worksheet.Cells["N2"].Value = "Part Description";
                worksheet.Cells["O2"].Value = "KGS - Weight";

                worksheet.Cells["P2"].Value = "Total Value";
                worksheet.Cells["Q2"].Value = "Currency Declared";
                worksheet.Cells["R2"].Value = "Origin Country Code";

                worksheet.Cells["S2"].Value = "CFSP - Supplementary Declaration Number";
                worksheet.Cells["T2"].Value = "CFSP - Supplementary Declaration Date";
                worksheet.Cells["U2"].Value = "CPC Home-Use";

                worksheet.Cells["A2:U2"].Style.WrapText = true;
                worksheet.Cells["A2:U2"].Style.Font.Bold = true;
                worksheet.Cells["A2:U2"].Style.Font.UnderLine = true;

                worksheet.Cells["A2:U2"].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2:U2"].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2:U2"].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2:U2"].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                worksheet.Cells["A2:U2"].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
                worksheet.Cells["A2:U2"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                row = 3;
                foreach (var consignment in consignments)
                {
                    var commodity = await consignment.Commodities.FirstOrDefault();

                    worksheet.Cells[row, 1].Value = consignment.CFSPShipmentNumber;
                    worksheet.Cells[row, 2].Value = consignment.Shipment.VehicleNumber;
                    worksheet.Cells[row, 3].Value = consignment.Shipment.TrailerNumber;

                    worksheet.Cells[row, 4].Value = consignment.EntryReference;
                    worksheet.Cells[row, 5].Value = consignment.UCR;
                    worksheet.Cells[row, 6].Value = LocalTime.Now.ToString("dd/MM/yyyy");

                    worksheet.Cells[row, 7].Value = consignment.Shipment.ExpectedDate.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 8].Value = consignment.Partner.Name;
                    worksheet.Cells[row, 9].Value = consignment.UKTrader.Name;

                    worksheet.Cells[row, 10].Value = consignment.Shipment.TrackingNumber;
                    worksheet.Cells[row, 11].Value = consignment.TotalPackages.ToString();
                    worksheet.Cells[row, 12].Value = consignment.InvoiceNumber;

                    worksheet.Cells[row, 13].Value = commodity?.Product?.CommodityCode.ToString();
                    worksheet.Cells[row, 14].Value = commodity?.DescriptionOfGoods;
                    worksheet.Cells[row, 15].Value = consignment.TotalNetWeight.ToString();

                    worksheet.Cells[row, 16].Value = consignment.TotalValue.ToString();
                    worksheet.Cells[row, 17].Value = consignment.InvoiceCurrency.Name;
                    worksheet.Cells[row, 18].Value = commodity?.CountryOfDestination.Code;

                    worksheet.Cells[row, 19].Value = consignment.CFSPShipmentNumber;
                    worksheet.Cells[row, 20].Value = LocalTime.Now.ToString("dd/MM/yyyy");
                    worksheet.Cells[row, 21].Value = consignment.SpecialCPC?.Number ?? "4000 000";

                    for (int i = 1; i < 22; i++)
                    {
                        worksheet.Cells[row, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        worksheet.Cells[row, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }


                    row++;
                }
                stream.Position = 0;
                xlPackage.Save();
                return new Blob(stream.ToArray(), "CFSP-" + LocalTime.Now.ToString("dd/MM/yyyy") + ".xlsx");
            }

        }
        public async Task ArchiveWithImporter()
        {
            using (var scope = Database.CreateTransactionScope())
            {
                try
                {
                    var service = Context.Current.GetService<IArchiveLogService>();
                    var currentUser = await Database.Of<Person>().Where(x => x.Email == "admin@uat.co").FirstOrDefault();
                    var shipments = await Database.Of<Shipment>()
                        .Where(x => !x.IsDeactivated && x.Progress.IsAnyOf(Progress.WithImporter, Progress.Partial))
                        .GetList()
                        .ToList();

                    var ip = await service.GetIPAddress();
                    var logId = new ArchiveLog();

                    foreach (var shipment in shipments)
                    {
                        var consigments = await shipment.Consignments.GetList().ToList();
                        if (consigments.IsSingle() && consigments.Any(x => x.ProgressId == Progress.WithImporter))
                        {
                            logId = await service.CreateArchiveLog("Archived due Shipment Status of: With Importer", currentUser as User, ip, shipment);
                            await CancelDeclaration(consigments.FirstOrDefault(), isArchive: true);
                            await Context.Current.Database().Update(await Database.Reload(shipment), x =>
                            {
                                x.IsDeactivated = true;
                                x.ArchiveLogIds += logId.ToStringOrEmpty() + ",";
                            });
                        }
                        else if (consigments.Skip(1).Any())
                        {
                            foreach (var item in consigments)
                            {
                                if (item.ProgressId == Progress.WithImporter)
                                    await CancelDeclaration(item, isArchive: true);
                            }
                        }

                    }
                    scope.Complete();
                }
                catch (Olive.Entities.ValidationException ex)
                {
                    throw ex;
                }
            }
        }
        public async Task DeleteNotification(Shipment shipment)
        {
            var accountNotification = await Database.Of<AccountingNotification>().Where(l => l.ShipmentId == shipment).GetList();
            if (accountNotification.Any())
                await Database.Delete(accountNotification);

            var error = await Database.Of<ErrorLog>().Where(l => l.ShipmentId == shipment).GetList();
            if (error.Any())
                await Database.Delete(error);
        }
    }
}