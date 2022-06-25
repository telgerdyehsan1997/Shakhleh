using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Olive;
using Olive.Entities;
using APIContracts;
using System.Linq;
using System.Text;

namespace Domain
{
    public interface IShipmentService : ILoggable
    {
        Task<string> Login(string userName, string password);

        Task<string> CreateEADShipment(ShipmentContract shipmentContract, string username);

    }


    public class ShipmentService : IShipmentService
    {
        IDatabase Database;
        IJWTProvider _JWTProvider;
        public ShipmentService(IDatabase database, IJWTProvider jWTProvider)
        {
            Database = database;
            _JWTProvider = jWTProvider;
        }

        public async Task<string> Login(string userName, string password)
        {
            var company = await Database.FirstOrDefault<Company>(x => x.Username == userName && x.Password == password);
            if (company == null)
                throw new UnauthorizedAccessException("Username and password are not correct.");

            return _JWTProvider.Of(JWTProviderType.Shipment).GenerateToken(userName);
        }

        public async Task<string> CreateEADShipment(ShipmentContract shipmentContract, string userName)
        {
            var company = await Database.FirstOrDefault<Company>(x => x.Username == userName);
            if (company == null || !await company.CanDoEAD() || company.IsOnHold)
                throw new ValidationException("Company is not valid or does not exist.");

            await EADShipmentMapper.MapShipmentContract(shipmentContract, company);
            if (shipmentContract.Consignments.None() || shipmentContract.Consignments.Select(x => x.CommodityContracts.Count).None())
                throw new ValidationException("consignment or commodity contract not valid");

            using (var scope = Database.CreateTransactionScope())
            {
                try
                {

                    var shipment = await Database.Save(shipmentContract.ImportedShipment);
                    foreach (var attachment in shipmentContract.ImportedAttachments)
                    {
                        await Database.Save(attachment);
                    }
                    if (shipmentContract.Carrier != null && shipmentContract.IsIntoUK)
                    {
                        if (shipmentContract.ImportedCarrier.IsNew)
                        {
                            var carrier = await Database.Save(shipmentContract.ImportedCarrier);
                            await Database.Update(shipment, x => x.Carrier = carrier);
                        }else
                            await Database.Update(shipment, x => x.Carrier = shipmentContract.ImportedCarrier);
                    }

                    foreach (var consignment in shipmentContract.Consignments)
                    {
                        if (consignment.ImportedUkTrader.IsNew)
                            consignment.ImportedUkTrader = await Database.Save(consignment.ImportedUkTrader);
                        else
                            await Database.Update(consignment.ImportedUkTrader, x => x.BranchIdentifier = consignment.UKTrader.BranchIdentifier);

                        if (consignment.ImportedPartner.IsNew)
                            consignment.ImportedPartner = await Database.Save(consignment.ImportedPartner);
                        else
                            await Database.Update(consignment.ImportedPartner, x => x.BranchIdentifier = consignment.Partner.BranchIdentifier);

                        if (consignment.ImportedDeclarant.IsNew)
                            consignment.ImportedDeclarant = await Database.Save(consignment.ImportedDeclarant);
                        else
                            await Database.Update(consignment.ImportedDeclarant, x => x.BranchIdentifier = consignment.Declarant.BranchIdentifier);

                        consignment.ImportedConsignment.IdNumber = await consignment.ImportedConsignment.GenerateIdNumber(shipment);
                        consignment.ImportedConsignment.ConsignmentNumber = await consignment.ImportedConsignment.GenerateConsignmentNumber(shipment);

                        consignment.ImportedConsignment.UCR = consignment.ImportedConsignment.CreateUCR(consignment.ImportedConsignment.Declarant.EORINumber, consignment.ImportedConsignment.ConsignmentNumber);

                        var savedConsignment = await Database.Save(consignment.ImportedConsignment);

                        foreach (var commodity in consignment.CommodityContracts)
                        {
                            if (commodity.ImportedProduct.IsNew)
                                commodity.ImportedProduct = await Database.Save(commodity.ImportedProduct);

                            //first product cause issue in mapping so need to apply here .
                            if (commodity.ImportedCommodity.HasDutyPayable)
                            {
                                var uktraderCondition = consignment.UKTrader.PaymentCode.IsEmpty() || consignment.UKTrader.DefermentNumber.IsEmpty();
                                var declarantCondition = consignment.Declarant.PaymentCode.IsEmpty() || consignment.Declarant.DefermentNumber.IsEmpty();
                                if (shipment.TypeId == ShipmentType.IntoUk && (uktraderCondition && declarantCondition))
                                {
                                    throw new Olive.Entities.ValidationException($"Commodidty code is {commodity.Product.CommodityExportCode} is duty payable, please add payment type and deferment number for at least one (UKTrader/Declarant).");
                                }
                            }

                            commodity.ImportedCommodity.Consignment = savedConsignment;
                            await Database.Save(commodity.ImportedCommodity);
                        }
                        if (shipmentContract.IsDraft)
                            await savedConsignment.FlagAsDraft();
                        else
                            await savedConsignment.FlagAsCompleted();
                    }
                    scope.Complete();

                    return shipment.TrackingNumber;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private async Task<Company> GetGuarantor(Consignment consignment)
        {
            var shipmentCompany = consignment.Shipment.Company;

            if (shipmentCompany != null)
            {
                if (shipmentCompany.GuarantorTypeId == GuarantorType.Own)
                    return shipmentCompany;

                if (shipmentCompany.GuarantorTypeId == GuarantorType.DifferentCompanyGuarantee)
                    return shipmentCompany.GuarantorName;
            }
            var ukTradder = consignment.UKTrader;

            if (ukTradder != null)
            {
                if (ukTradder.GuarantorTypeId == GuarantorType.Own)
                    return ukTradder;

                if (ukTradder.GuarantorTypeId == GuarantorType.DifferentCompanyGuarantee)
                    return ukTradder.GuarantorName;
            }

            return await Company.ChannelPort;
        }

        public async Task Log(string str, LogType logType)
        {
            try
            {
                await Database.Save(new ShipmentApiTransactionLog
                {
                    Type = logType,
                    File = new Blob(Encoding.ASCII.GetBytes(str), LocalTime.Now.ToLongTimeString())
                });

            }
            catch (Exception ex)
            {
                Olive.Log.For<ShipmentApiTransactionLog>().Error(ex);
            }
        }


    }
}

