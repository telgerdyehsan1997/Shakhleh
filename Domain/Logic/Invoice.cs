using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Domain
{
    public partial class Invoice
    {

        public bool IsInvoiceExcelFileVisibleTo(IPrincipal user) => true;

        public bool IsInvoicePdfFileVisibleTo(IPrincipal user) => true;

    }
}
