using MSharp;

namespace Admin.Company
{
    class UKTradersPartnersPage : SubPage<CompaniesPage>
    {
        public UKTradersPartnersPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");

            Add<Modules.CompanyUKTraderPartnerList>();
        }
    }
}