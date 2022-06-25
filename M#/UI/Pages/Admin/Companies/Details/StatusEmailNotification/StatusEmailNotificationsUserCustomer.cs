using MSharp;

namespace Admin.Company
{
    class StatusEmailNotificationsUserCustomerPage :  SubPage<CompaniesPage>
    {
        public StatusEmailNotificationsUserCustomerPage()
        {
            Roles(AppRole.Admin);
            Set(PageSettings.LeftMenu, "CompanyMenu");
            Add<Modules.StatusEmailNotificationsUserCustomerShipmentList>();
        }
    }
}