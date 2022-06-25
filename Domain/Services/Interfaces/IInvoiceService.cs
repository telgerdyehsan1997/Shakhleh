using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IInvoiceService
    {

        Task CreateInvoiceJobs();

        Task GenerateInvoices();

        Task SendInvoices(IEnumerable<Invoice> invoices);

        Task<IEnumerable<Invoice>> GetInvoices(int? year = null, int? month = null, InvoiceType type = null, Company customer = null);

        Task GenerateInvoiceFiles();
    }
}
