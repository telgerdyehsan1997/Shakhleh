using MSharp;

namespace Admin.Accounting
{
    class InvoicesPage : SubPage<Admin.AccountingPage>
    {
        public InvoicesPage()
        {
            Add<Modules.InvoiceList>();
            BaseController("MFABaseController");
        }
    }
}