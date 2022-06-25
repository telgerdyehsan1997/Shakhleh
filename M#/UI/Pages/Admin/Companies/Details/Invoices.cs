using MSharp;

namespace Admin.Company
{
    class InvoicesPage : SubPage<CompaniesPage>
    {
        public InvoicesPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");
            Add<Modules.CompanyInvoiceList>();
        }
    }
}