using MSharp;
using Domain;

namespace Modules
{
    public class CustomerSettingsMenu : MenuModule
    {
        public CustomerSettingsMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");
            ViewModelProperty<CompanyUser>("user").FromRequestParam("user");

            Item("Contacts")
              .OnClick(x => x.Go<Customer.Settings.CustomerContactsPage>());

            Item("Contact Groups")
              .OnClick(x => x.Go<Customer.Settings.CustomerContactGroupsPage>());

            Item("Details")
                .OnClick(x => x.Go<Customer.Settings.RecordPage>());

            Item("Status Email Notifications").OnClick(x => x.Go<Customer.Settings.StatusEmailNotificationsUserCustomerPage>());

            Item("Users")
                .OnClick(x => x.Go<Customer.Settings.UsersPage>());

        }
    }
}