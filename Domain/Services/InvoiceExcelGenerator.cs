using OfficeOpenXml;
using Olive;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace Domain
{
    public interface IInvoiceExcelGenerator
    {
        Task<byte[]> GetExcelBytes(Invoice invoice);
    }

    public class InvoiceExcelGenerator : IInvoiceExcelGenerator
    {
        public const int Size = 16;
        public async Task<byte[]> GetExcelBytes(Invoice invoice)
        {
            if (invoice.TypeId == InvoiceType.Charge)
            {
                return await GenerateChargeExcel(invoice);
            }
            else
            {
                return await GenerateTransactionExcel(invoice);
            }
        }

        private async Task<byte[]> GenerateChargeExcel(Invoice invoice)
        {
            var list = new List<ChargeList>();

            list.Add(new ChargeList
            {
                License = invoice.Charge.Name,
                ValidFrom = invoice.InvoicePeriodStartDate.ToString("dd/MM/yyyy"),
                ValidTo = invoice.InvoicePeriodEndDate.ToString("dd/MM/yyyy"),
                Value = invoice.Total.ToString("C", invoice.Charge.CurrencyId == ChargeCurrencyOption.Euro ? CultureInfo.GetCultureInfo("en-ie") : CultureInfo.GetCultureInfo("en-GB"))
            });

            using (var excelPkg = new ExcelPackage())
            {
                var wsSheet1 = excelPkg.Workbook.Worksheets.Add("Sheet1");
                using (ExcelRange Rng = wsSheet1.Cells[1, 1, 1, 1])
                {
                    Rng.Value = "License invoice";
                    Rng.Style.Font.Size = Size;
                    Rng.Style.Font.Bold = true;
                    Rng.Style.Font.Italic = true;
                }
                wsSheet1.Protection.IsProtected = false;
                wsSheet1.Protection.AllowSelectLockedCells = false;
                wsSheet1.Cells["A3"].LoadFromCollection(list, PrintHeaders: true, OfficeOpenXml.Table.TableStyles.Medium2);

                using (var memory = new MemoryStream())
                {
                    excelPkg.SaveAs(memory);

                    return memory.ToArray();
                }
            }
        }

        private async Task<byte[]> GenerateTransactionExcel(Invoice invoice)
        {

            var list = await invoice.Transactions
                .GetList()
                .Select(x => new TransactionList
                {
                    ShipmentTrackingNumber = x.ShipmentTrackingNumber,
                    CustomerReference = x.CustomerReference,
                    NumberOfConsignments = x.NumberOfConsignments,
                    VehicleNumber = x.VehicleNumber,
                    Value = invoice.Total.ToString("C", invoice.Charge.CurrencyId == ChargeCurrencyOption.Euro ? CultureInfo.GetCultureInfo("en-ie") : CultureInfo.GetCultureInfo("en-GB"))
                });

            using (var excelPkg = new ExcelPackage())
            {
                var wsSheet1 = excelPkg.Workbook.Worksheets.Add("Sheet1");
                using (ExcelRange Rng = wsSheet1.Cells[1, 1, 1, 1])
                {
                    Rng.Value = "Additional Consignments invoice";
                    Rng.Style.Font.Size = Size;
                    Rng.Style.Font.Bold = true;
                    Rng.Style.Font.Italic = true;
                }
                wsSheet1.Protection.IsProtected = false;
                wsSheet1.Protection.AllowSelectLockedCells = false;
                wsSheet1.Cells["A3"].LoadFromCollection(list, PrintHeaders: true, OfficeOpenXml.Table.TableStyles.Medium2);

                using (var memory = new MemoryStream())
                {
                    excelPkg.SaveAs(memory);

                    return memory.ToArray();
                }
            }
        }

    }

    public class ChargeList
    {
        public string License { get; set; }
        public string ValidFrom { get; set; }
        public string ValidTo { get; set; }
        public string Value { get; set; }
    }


    public class TransactionList
    {
        public string ShipmentTrackingNumber { get; set; }
        public string CustomerReference { get; set; }
        public string VehicleNumber { get; set; }
        public int NumberOfConsignments { get; set; }
        public string Value { get; set; }
    }
}
