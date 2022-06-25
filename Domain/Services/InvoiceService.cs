using Olive;
using Olive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class InvoiceService : IInvoiceService
    {
        IDatabase Database;
        ILookupService LookupService;
        IInvoiceExcelGenerator InvoiceExcelGenerator;
        IInvoicePdfGenerator InvoicePdfGenerator;

        public InvoiceService(IDatabase database, ILookupService lookupService, IInvoiceExcelGenerator invoiceExcelGenerator, IInvoicePdfGenerator invoicePdfGenerator)
        {
            Database = database;
            LookupService = lookupService;
            InvoiceExcelGenerator = invoiceExcelGenerator;
            InvoicePdfGenerator = invoicePdfGenerator;
        }

        public async Task CreateInvoiceJobs()
        {
            var chargeMonth = LocalTime.Now.AddMonths(1).GetEndOfMonth();
            var transactionMonth = LocalTime.Now.AddMonths(-1).GetEndOfMonth();
            var printDate = LocalTime.Now.GetBeginningOfMonth();
            var indicatorMonth = LocalTime.Now.Month;

            var chargeInvoice = await Database.Of<InvoiceJob>().Any(t => t.IndicatorMonth == indicatorMonth && t.InvoiceTypeId == InvoiceType.Charge);

            if (!chargeInvoice)
                await Database.Save(new InvoiceJob
                {
                    IndicatorMonth = indicatorMonth,
                    InvoiceType = InvoiceType.Charge,
                    Status = InvoiceJobStatus.NotStarted,
                    InvoiceToDate = chargeMonth,
                });

            var transactionInvoice = await Database.Of<InvoiceJob>().Any(t => t.IndicatorMonth == indicatorMonth && t.InvoiceTypeId == InvoiceType.Transaction);

            if (!transactionInvoice)
                await Database.Save(new InvoiceJob
                {
                    IndicatorMonth = indicatorMonth,
                    InvoiceType = InvoiceType.Transaction,
                    Status = InvoiceJobStatus.NotStarted,
                    InvoiceToDate = transactionMonth,
                });
        }

        public async Task GenerateInvoices()
        {
            var jobs = await Database.Of<InvoiceJob>().Where(t => t.StatusId == InvoiceJobStatus.NotStarted).GetList();

            await jobs.DoAsync(async (job, _) =>
            {
                if (job.InvoiceTypeId == InvoiceType.Charge)
                    await GenerateChargeInvoices(job);

                else if (job.InvoiceTypeId == InvoiceType.Transaction)
                    await GenerateTransactionInvoices(job);
            });
        }

        private async Task GenerateTransactionInvoices(InvoiceJob job)
        {
            try
            {
                job = await Database.Update(job.Clone(), t => t.Status = InvoiceJobStatus.InProgress);
                await Database.Update(job.Clone(), t => t.Status = InvoiceJobStatus.Done);
            }
            catch (Exception ex)
            {
                Log.For<InvoiceJobStatus>().Error(ex);
                await Database.Update(job.Clone(), t => t.Status = InvoiceJobStatus.Error);
            }
        }

        private async Task GenerateChargeInvoices(InvoiceJob job)
        {
            try
            {
                job = await Database.Update(job.Clone(), t => t.Status = InvoiceJobStatus.InProgress);
                await GenerateChargeInvoices();
                await Database.Update(job.Clone(), t => t.Status = InvoiceJobStatus.Done);
            }
            catch (Exception ex)
            {
                Log.For<Invoice>().Error(ex);
                await Database.Update(job.Clone(), t => t.Status = InvoiceJobStatus.Error);
            }
        }

        public async Task GenerateChargeInvoices()
        {
            var companies = await LookupService.GetActiveCompanyList().ToList();

            foreach (var company in companies)
                await GenerateCompanyChargeInvoice(company);
        }


        async Task GenerateCompanyChargeInvoice(Company company)
        {
            var isYearly = !company.IsMonthlyInvoice && LocalTime.Now.Month >= company.LicenseFeeInvoicingStartMonth.MonthNumber - 1;

            if (!company.IsMonthlyInvoice && !isYearly) return;

            int? monthIndicator = null;
            if (company.IsMonthlyInvoice)
                monthIndicator = LocalTime.Now.Month + 1;

            var yearIndicator = LocalTime.Now.Year;

            var charge = GetCharge(company);
            if (charge == null) return;


            var start = GetStartDate(company, isForCharge: true);
            var end = GetEndDate(company, isForCharge: true);
            var vat = await GetVat(company);

            var monthlyExist = await Invoice.FindByCompanyAndTypeAndInvoiceMonthAndInvoiceYear(company, InvoiceType.Charge, monthIndicator, yearIndicator) != null;
            var yearlyExist = await Invoice.FindByCompanyAndTypeAndInvoiceMonthAndInvoiceYear(company, InvoiceType.Charge, null, yearIndicator) != null;

            const int days = 28;
            if ((company.IsMonthlyInvoice && !monthlyExist) || (!company.IsMonthlyInvoice && !yearlyExist))
            {
                using (var scope = Database.CreateTransactionScope())
                {
                    var invoice = await Database.Save(new Invoice
                    {
                        Charge = charge,
                        Company = company,
                        InvoiceMonth = monthIndicator,
                        InvoiceYear = yearIndicator,
                        Type = InvoiceType.Charge,
                        InvoicePeriodStartDate = start,
                        InvoicePeriodEndDate = end,
                        DueDate = new DateTime(LocalTime.Now.Year, LocalTime.Now.Month, days),
                        TotalNet = charge.LicenseFee,
                        TotalVat = charge.LicenseFee * vat.value / 100,
                        Total = charge.LicenseFee + (charge.LicenseFee * vat.value / 100),
                        PrintDate = LocalTime.Now.GetBeginningOfMonth(),
                        GenerateAt = LocalTime.Now,
                    });

                    await Database.Save(new ChargeInvoiceRow
                    {
                        Invoice = invoice,
                        NetValue = charge.LicenseFee,
                        VatValue = charge.LicenseFee * vat.value / 100,
                        TotalValue = charge.LicenseFee + (charge.LicenseFee * vat.value / 100),
                        TaxRate = vat.rate,
                    });
                    scope.Complete();
                }
            }
        }

        async Task<Invoice> GeneateFiles(Invoice invoice)
        {
            var fileName = $"Invoice-{invoice.InvoiceNumber}-{LocalTime.Now.ToString("dd-MM-yyyy-HH-mm-ss")}";
            var pdf = new Blob(await InvoicePdfGenerator.GetPdfBytes(invoice), $"{fileName}.pdf");
            var excel = new Blob(await InvoiceExcelGenerator.GetExcelBytes(invoice), $"{fileName}.xlsx");

            return await Database.Update(invoice, x =>
            {
                x.InvoiceExcelFile = excel;
                x.InvoicePdfFile = pdf;
                x.Status = InvoiceStatus.NotSentToExchequer;
            });
        }

        Charge GetCharge(Company company, DateTime? date = null)
        {
            return company.InvoiceCharge;
        }

        async Task<(string rate, int value)> GetVat(Company company)
        {
            var vat = (await Database.Of<VATRate>().Where(t => !t.IsDeactivated).GetList()).ToList().LastOrDefault();
            return company.Country.Code == "GB" ? ("S", vat.RateS) : ("Z", vat.RateZ);
        }

        DateTime GetEndDate(Company company, bool isForCharge)
        {
            var indicator = isForCharge ? 1 : -1;

            if (company.IsMonthlyInvoice)
                return LocalTime.Now.AddMonths(indicator).GetEndOfMonth();

            return new DateTime(LocalTime.Now.Year + 1, company.LicenseFeeInvoicingStartMonth.MonthNumber, 1);
        }

        DateTime GetStartDate(Company company, bool isForCharge)
        {
            var indicator = isForCharge ? 1 : -1;
            if (company.IsMonthlyInvoice)
                return LocalTime.Now.AddMonths(indicator).GetBeginningOfMonth();

            return new DateTime(LocalTime.Now.Year, company.LicenseFeeInvoicingStartMonth.MonthNumber, 1);
        }

        public async Task SendInvoices(IEnumerable<Invoice> invoices)
        {
            await Database.Update(invoices, i => i.Status = InvoiceStatus.SentToExchequer);
        }

        public async Task<IEnumerable<Invoice>> GetInvoices(int? year = null, int? month = null, InvoiceType type = null, Company customer = null)
        {
            var query = Database.Of<Invoice>();

            if (year.HasValue)
                query = query.Where(x => x.InvoiceYear == year);
            if (month.HasValue)
                query = query.Where(x => x.InvoiceMonth == month);
            if (type != null)
                query = query.Where(x => x.TypeId == type);
            if (customer != null)
                query = query.Where(x => x.CompanyId == customer);

            return await query.GetList();
        }

        public async Task GenerateInvoiceFiles()
        {
            var invoices = await Database.GetList<Invoice>(x => x.StatusId == InvoiceStatus.InProgress);

            await invoices.ForEachAsync(degreeOfParallelism: 3, async invoice =>
             {
                 await GeneateFiles(invoice);
             });
        }
    }

    public class EADInvoiceVM
    {
        public EADInvoiceVM()
        {
            Consignments = new List<Consignment>();
        }

        public Shipment Shipment { get; set; }
        public List<Consignment> Consignments { get; set; }
    }

    public class NCTSInvoiceVM
    {
        public NCTSInvoiceVM()
        {
        }
    }
}
