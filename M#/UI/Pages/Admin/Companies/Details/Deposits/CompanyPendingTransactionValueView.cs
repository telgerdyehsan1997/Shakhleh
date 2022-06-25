using MSharp;

namespace Admin.Company
{
    class CompanyPendingTransactionValueViewPage : RootPage
    {
        public CompanyPendingTransactionValueViewPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");
            Roles(AppRole.Admin);
            Add<Modules.CompanyPendingTransactionValueView>();
        }
    }
}