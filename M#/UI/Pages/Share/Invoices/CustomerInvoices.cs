using MSharp;

namespace Share.Invoices
{
    class CustomerInvoicesPage : RootPage
    {
        public CustomerInvoicesPage()
        {
            Add<Modules.CustomerInvoiceList>();
        }
    }
}