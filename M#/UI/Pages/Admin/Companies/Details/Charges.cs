using MSharp;

namespace Admin.Company
{
    class ChargesPage : SubPage<CompaniesPage>
    {
        public ChargesPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");
            Add<Modules.ChargeList>();
            BaseController("MFABaseController");
        }
    }
}