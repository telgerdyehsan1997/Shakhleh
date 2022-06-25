using MSharp;

namespace Admin.Company
{
    class DepositsPage : SubPage<CompaniesPage>
    {
        public DepositsPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");

            //Add<Modules.CompanyDepositView>(); Invoked asynchronously on CompanyDepositList
            Add<Modules.CompanyDepositList>();
            BaseController("MFABaseController");
        }
    }
}