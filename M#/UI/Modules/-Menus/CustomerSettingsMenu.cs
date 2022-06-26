using MSharp;
using Domain;

namespace Modules
{
    public class CustomerSettingsMenu : MenuModule
    {
        public CustomerSettingsMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");
            //ViewModelProperty<CompanyUser>("user").FromRequestParam("user");

           
            //Item("Users")
            //    .OnClick(x => x.Go<Customer.Settings.UsersPage>());

        }
    }
}