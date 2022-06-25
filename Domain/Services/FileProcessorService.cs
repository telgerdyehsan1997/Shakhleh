using Microsoft.Extensions.Logging;
using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace Domain
{
    public interface IFileProcessorService
    {

    }

    public class FileProcessorService : IFileProcessorService
    {
        IDatabase Database;
        ILogger Logger;
        IEADShipmentService EADShipmentService;
        IShipmentService ShipmentService;

        string UnprocessedFolderPath;
        string ProcessedWithErrorFolderPath;

        string ProcessedDTIFolderPath;
        string ProcessedWithErrorDTIFolderPath;

        string ProcessedNonDTIFolderPath;
        string ProcessedWithErrorNonDTIFolderPath;

        string ProcessedIEFolderPath;
        string ProcessedWithErrorIEFolderPath;

        string ProcessedNonIEFolderPath;
        string ProcessedWithErrorNonIEFolderPath;

        string ProcessedUnknownPath;

        string ProcessedC130Path;

        bool IsManual = false;

        public FileProcessorService(IDatabase database, ILogger<FileProcessorService> logger, IEADShipmentService eadShipmentService, IShipmentService shipmentService)
        {
            Database = database;
            Logger = logger;
            EADShipmentService = eadShipmentService;
            ShipmentService = shipmentService;

            UnprocessedFolderPath = Config.Get<string>("FileProcessor:UnprocessedFolderPath");
            ProcessedWithErrorFolderPath = Config.Get<string>("FileProcessor:ProcessedWithErrorFolderPath");

            ProcessedDTIFolderPath = Config.Get<string>("FileProcessor:DTI:ProcessedFolderPath");
            ProcessedWithErrorDTIFolderPath = Config.Get<string>("FileProcessor:DTI:ProcessedWithErrorFolderPath");

            ProcessedNonDTIFolderPath = Config.Get<string>("FileProcessor:NonDTI:ProcessedFolderPath");
            ProcessedWithErrorNonDTIFolderPath = Config.Get<string>("FileProcessor:NonDTI:ProcessedWithErrorFolderPath");

            ProcessedIEFolderPath = Config.Get<string>("FileProcessor:IE:ProcessedFolderPath");
            ProcessedWithErrorIEFolderPath = Config.Get<string>("FileProcessor:IE:ProcessedWithErrorFolderPath");

            ProcessedNonIEFolderPath = Config.Get<string>("FileProcessor:NonIE:ProcessedFolderPath");
            ProcessedWithErrorNonIEFolderPath = Config.Get<string>("FileProcessor:NonIE:ProcessedWithErrorFolderPath");

            ProcessedUnknownPath = Config.Get<string>("FileProcessor:Unknown:ProcessedFolderPath");

            ProcessedC130Path = Config.Get<string>("FileProcessor:C130:ProcessedFolderPath");
        }



        static async Task<List<Blob>> ReadFiles(string fileName)
        {
            var files = new List<Blob>();
            using (var file = File.OpenRead(fileName))
            using (var zip = new ZipArchive(file, ZipArchiveMode.Read))
            {
                foreach (var entry in zip.Entries)
                {
                    using (var stream = entry.Open())
                    {
                        var fileData = await stream.ReadAllBytesAsync();
                        var fileNameWithoutPath = Path.GetFileName(entry.FullName);
                        files.Add(new Blob(fileData, fileNameWithoutPath));
                    }
                }
            }
            return files;
        }

        static async Task<List<Blob>> ReadFiles(Blob blob)
        {
            var files = new List<Blob>();
            var bytes = await blob.GetFileDataAsync();
            using (var zipBlobFileStream = new MemoryStream())
            using (var zip = new ZipArchive(new MemoryStream(bytes)))
            {
                foreach (var entry in zip.Entries)
                {
                    using (var stream = entry.Open())
                    {
                        if (entry.Length > 0)
                        {
                            files.Add(new Blob(entry.Open().ReadAllBytes(), entry.FullName.Split("/").Last()));
                        }
                    }
                }
                return files;
            }
        }

        void ProcessUnknown(string fileName)
        {
            MoveFile(fileName, ProcessedUnknownPath);
        }

        void ProcessC130(string fileName)
        {
            MoveFile(fileName, ProcessedC130Path);
        }

        async Task<(Consignment Consignment, string UCR, string TraderReference, string EntryReference, string ExportMrn)> ProcessDTIXml(byte[] xmlFileData)
        {
            using (var stream = new MemoryStream(xmlFileData))
            {
                var document = new XmlDocument();
                document.Load(stream);

                var entryReference = document.SelectSingleNode("//document/references/entryReference")?.InnerText;
                var declarationUcr = document.SelectSingleNode("//document/references/declarationUcr")?.InnerText;
                var traderReference = document.SelectSingleNode("//document/references/traderReference")?.InnerText;
                var exportMrn = document.SelectSingleNode("//document/references/exportMrn")?.InnerText;

                Consignment consignment = null;
                if (declarationUcr.HasValue())
                {
                    declarationUcr = StripEndAs(declarationUcr);
                    consignment = await Consignment.FindByUCR(declarationUcr);

                }

                if (declarationUcr.HasValue() && consignment == null)
                {
                    var trackNumber = declarationUcr.Split("-")[1];
                    if (trackNumber.HasValue())
                    {
                        consignment = await Consignment.FindByConsignmentNumber(trackNumber);
                    }
                }

                if (entryReference.HasValue() && consignment == null)
                    consignment = await Consignment.FindByEntryReference(entryReference);

                return (consignment, declarationUcr, traderReference, entryReference, exportMrn);
            }
        }

        async Task<(Consignment Consignment, string UCR, string ExportMRN, string EntryReference)> ProcessNonDTIXml(byte[] xmlFileData)
        {
            using (var stream = new MemoryStream(xmlFileData))
            {
                var document = new XmlDocument();
                document.Load(stream);

                var entryReference = document.SelectSingleNode("//document/references/entryReference")?.InnerText;
                var declarationUcr = document.SelectSingleNode("//document/references/declarationUcr")?.InnerText;
                var exportMrn = document.SelectSingleNode("//document/references/exportMrn")?.InnerText;

                Consignment consignment = null;
                if (declarationUcr.HasValue())
                    consignment = await Consignment.FindByUCR(declarationUcr);

                if (declarationUcr.HasValue() && consignment == null)
                {
                    var trackNumber = declarationUcr.Split("-")[1];
                    if (trackNumber.HasValue())
                    {
                        trackNumber = StripEndAs(trackNumber);
                        consignment = await Consignment.FindByConsignmentNumber(trackNumber);
                    }
                }

                if (consignment == null && entryReference.HasValue())
                    consignment = await Consignment.FindByEntryReference(entryReference);

                return (consignment, declarationUcr, exportMrn, entryReference);
            }
        }

        async Task<(string TotalDuty, string TotalVatPaid, string TotalVat, string TotalOther)> ExtractInfoFromNonDTIPdf(IEnumerable<Blob> filesInZip)
        {
            string totalDtyName = "Total Dty",
                totalVatPaidName = "Total VAT Paid",
                totalVatName = "Total VAT (PVA)",
                totalOtherName = "Total Other";
            string totalDuty = null, totalVatPaid = null, totalVat = null, totalOther = null;

            var file = filesInZip.FirstOrDefault(x => x.FileExtension == ".pdf" && x.FileName.ToUpper().StartsWithAny("C88"));
            if (file == null)
                return (null, null, null, null);

            var reader = new iTextSharp.text.pdf.PdfReader(await file.GetFileDataAsync());
            var page = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1);
            var lines = page.Split('\n');
            foreach (var line in lines)
            {
                if (line.Contains(totalDtyName))
                    totalDuty = line.Remove(":").Replace(totalDtyName, "").Trim();
                if (line.Contains(totalVatPaidName))
                    totalVatPaid = line.Remove(":").Replace(totalVatPaidName, "").Trim();
                if (line.Contains(totalVatName))
                    totalVat = line.Remove(":").Replace(totalVatName, "").Trim();
                if (line.Contains(totalOtherName))
                    totalOther = line.Remove(":").Replace(totalOtherName, "").Trim();
            }

            return (totalDuty, totalVatPaid, totalVat, totalOther);
        }

        async Task UpdateDTIProgress(Consignment consignment, string code, IEnumerable<Blob> files)
        {
            var isInUK = consignment.Shipment.IsInUK;
            var defermentNumber = consignment.GetDefermentNumber();
            var fileData = await (files.FirstOrDefault(x => x.FileExtension == ".pdf")?.GetFileDataAsync() ?? null);
            if ((code == "H2" && isInUK && !defermentNumber.Number.StartsWith("2")) || (code == "P2" && !isInUK))
            {
                consignment = await EADShipmentService.CheckRouteOnly(consignment);
                await consignment.FlagAsAwaitingArrivalDeparture();
            }
            else if (code == "H2" && isInUK && defermentNumber.Number.StartsWith("2"))
            {
                consignment = await EADShipmentService.CheckRouteOnly(consignment);
                var revenue = await GetTotalDeferredRevenue(fileData);
                if (revenue > 0m)
                {
                    await Deposit.AddPending(consignment, revenue);

                    if (await Deposit.HasCompanyRemainingBalanceZeroOrPositive(consignment.Shipment.Company))
                        await consignment.FlagAsAwaitingArrivalDeparture();
                    else
                        await consignment.FlagAsDutyPaymentWithAmendment();
                }
                else
                {
                    await consignment.FlagAsAwaitingArrivalDeparture();
                }
            }
            else if (isInUK && code == "E2")
            {
                consignment = await EADShipmentService.CheckRouteStatus(consignment, code);

                if (defermentNumber.Number.StartsWith("2"))
                {
                    var revenue = await GetTotalDeferredRevenue(fileData);
                    await Deposit.UpdatePendingToWithdrawl(consignment, revenue);
                }
            }
            else if (code == "X2" && !isInUK)
            {
                consignment = await EADShipmentService.CheckRouteStatus(consignment);

                var x2File = files.FirstOrDefault(t => t.FileName.ToUpper().Contains("X2"));
                if (x2File != null)
                    await ConsignmentDocument.SendX2ToClient(consignment, x2File);
            }
            else if (code == "S8" && !isInUK)
            {
                await consignment.FlagAsLeftCountry();
            }
            else if (code.IsAnyOf("E9", "N6", "N3", "N4", "S6", "S3", "S4"))
            {
                if (
                    (isInUK && consignment.Route.IsAnyOf("3", "6")) ||
                    (!isInUK && consignment.Route.IsAnyOf("6"))
                    )
                    await consignment.FlagAsQueriedArrived();
                else
                    await consignment.FlagAsQueriedWithCustoms();
            }
            else if (code == "X6" && !isInUK)
            {
                await consignment.FlagAsCleared();
            }
            else if (code.IsAnyOf("H5", "P5"))
            {
                await consignment.FlagAsCanceled();
            }
            else
                throw new ValidationException($"Unknow code '{code}'.");

        }

        void MoveFile(string sourceFilePath, string destinationPath)
        {
            if (IsManual) return;

            var destinationFilePath = Path.Combine(destinationPath, Path.GetFileName(sourceFilePath));
            File.Move(sourceFilePath, destinationFilePath);
        }

        async Task LogReportError(string fileName, string error, string location, string stackTrace = null, Shipment shipment = null)
        {
            if (IsManual) return;

            var file = fileName.Split("\\").LastOrDefault();
            var log = await ReportErrorLog.FindByFileName(file);
            if (log != null)
                await Database.Delete(log);

            await Database.Save(new ReportErrorLog
            {
                FileName = file,
                Location = location.Remove(UnprocessedFolderPath).TrimStart("\\"),
                Error = error,
                StackTrace = stackTrace,
            });

            MoveFile(fileName, location);
        }

        public async Task<decimal> GetTotalDeferredRevenue(byte[] bytes)
        {
            if (bytes != null)
            {
                var reader = new iTextSharp.text.pdf.PdfReader(bytes);
                var numberOfPages = reader.NumberOfPages;

                for (var currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                {
                    var page = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, currentPageIndex);
                    var lines = page.Split('\n');
                    foreach (var line in lines)
                    {
                        if (line.Contains("Total deferred revenue"))
                        {
                            return Convert.ToDecimal(line.Split("Total deferred revenue")[1]);
                        }
                    }
                }
            }
            return 0;
        }
        private static string StripEndAs(string item)
        {

            bool found = false;
            if (item.Trim().EndsWith("A"))
            {
                int lastLocation = item.LastIndexOf("A");
                if (lastLocation >= 0)
                {
                    found = true;
                    item = item.Substring(0, lastLocation);
                }
            }
            if (found)
                item = StripEndAs(item);

            return item;
        }
    }
}
