using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public interface IExchequerService
    {
        Task SendInvoices(IEnumerable<Invoice> invoices);
    }
}
