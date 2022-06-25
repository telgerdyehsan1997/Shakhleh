using Olive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;
using System.Net;
using Olive.Entities.Data;
using Olive.Entities;
using System.Net.Http;

namespace Domain
{
    public class ExchequerService : IExchequerService
    {
        IDatabase Database;
        public ExchequerService(IDatabase database)
        {
            Database = database;
        }
        public async Task SendInvoices(IEnumerable<Invoice> invoices)
        {
            try
            {
                var csv = await GetCsv(invoices);
                //File.WriteAllBytes("ExchequerCsv.csv", csv);

                //using (var client = new WebClient())
                //{
                //    client.UploadString(Config.Get("ExchequerUrl"), "POST", csv);

                //}

                using (var client = new HttpClient())
                {
                    var requestMessage = new HttpRequestMessage(new HttpMethod("POST"), Config.Get("ExchequerUrl"));
                    requestMessage.Content = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("body", csv) });   // This is where your content gets added to the request body

                    await client.SendAsync(requestMessage);
                }
                await Database.Update(invoices, i => i.Status = InvoiceStatus.SentToExchequer);
            }
            catch (Exception ex)
            {
                Log.For<InvoiceStatus>().Error(ex);
                await Database.Update(invoices, i => i.Status = InvoiceStatus.NotSentToExchequerFailure);
            }

        }

        private async Task<string> GetCsv(IEnumerable<Invoice> invoices)
        {
            var csv = new StringBuilder();
            csv.AppendLine(GetCsvHeader());

            var invoiceItems = await invoices.SelectManyAsync(async x =>
            {
                var header = await GetInvoiceHeader(x);
                var items = await GetInvoiceLineItems(x);
                return items.Prepend(header);
            });
            csv.AppendLine(GetCsvLineItems(invoiceItems));

            return csv.ToString();
        }

        private string GetCsvLineItems(IEnumerable<InvoiceTH> invoiceItems)
        {
            var lineitems = new StringBuilder();
            foreach (var item in invoiceItems)
            {
                lineitems.AppendLine(GetInvoiceLineString(item));
            }

            return lineitems.ToString();
        }

        private string GetInvoiceLineString(InvoiceTH item)
        {
            return $"{item.RecordType},{item.TransactionType}," +
                $"{item.AccountCode},{item.Currency},{item.TransactionDate}," +
                $"{item.DueDate},{item.YourRef},{item.LongYourRef}," +
                $"{item.UserDefined2},{item.UserDefined3},{item.UserDefined5}" +
                $",{item.UserDefined6},{item.UserDefined7},{item.UserDefined8}";
        }

        private string GetCsvHeader()
        {
            return @"""RecordType"",""TransactionType"",""AccountCode"",""Currency"",""TransactionDate"",""DueDate"",""YourRef"",""LongYourRef"",""UserDefined2"",""UserDefined3"",""UserDefined5"",""UserDefined6"",""UserDefined7"",""UserDefined8""";
        }

        private async Task<InvoiceTH> GetInvoiceHeader(Invoice invoice)
        {
            return new InvoiceTH
            {
                RecordType = "TH",
                TransactionType = "SIN",
                AccountCode = invoice.Company.CustomerAccountNumber,
                Currency = GetCurrency(invoice.Charge.Currency),
                TransactionDate = invoice.GenerateAt.ToString("yyyyMMdd"),
                DueDate = invoice.PrintDate.ToString("yyyyMMdd"),
                YourRef = invoice.InvoiceNumber.ToString(),
                LongYourRef = "CustomsPro Licence",
                UserDefined8 = "Folkestone Services",
            };
        }
        private int GetCurrency(ChargeCurrencyOption currency)
        {
            if (currency == ChargeCurrencyOption.Pounds)
                return 1;
            return 2;
        }
        private async Task<IEnumerable<InvoiceTH>> GetInvoiceLineItems(Invoice invoice)
        {
            var exchequercode = await Database.GetList<ExchequerCode>().FirstOrDefault();
            var items = (await invoice.Charges.GetList()).ToList();
            return items.Select(x => new InvoiceTH
            {
                RecordType = "TL",
                TransactionType = exchequercode.Department, //Department
                AccountCode = "0", //discount amount
                Currency = items.IndexOf(x) + 1, //line no
                TransactionDate = false.ToString().ToUpper(), //paymentline
                DueDate = $"{invoice.Company.Name} - {invoice.Charge.Name}", // Line Description
                YourRef = x.NetValue.ToString(),
                LongYourRef = x.TaxRate,
                UserDefined2 = x.VatValue.ToString(),
                UserDefined3 = exchequercode.NominalCode,
                UserDefined5 = exchequercode.CostCentre,
                UserDefined6 = GetCurrency(invoice.Charge.Currency).ToString()
            });
        }
    }

    class InvoiceTH
    {
        public string RecordType { get; set; }
        public string TransactionType { get; set; }
        public string AccountCode { get; set; }
        public int Currency { get; set; }
        public string TransactionDate { get; set; }
        public string DueDate { get; set; }
        public string YourRef { get; set; }
        public string LongYourRef { get; set; }
        public string UserDefined2 { get; set; }
        public string UserDefined3 { get; set; }
        public string UserDefined5 { get; set; }
        public string UserDefined6 { get; set; }
        public string UserDefined7 { get; set; }
        public string UserDefined8 { get; set; }
    }
}
