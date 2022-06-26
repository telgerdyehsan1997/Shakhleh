using MSharp;
using Domain;

namespace Modules
{
    public class CompanyMenu : MenuModule
    {
        public CompanyMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");


            //Item("Status Email Notifications")
            //      .VisibleIf("info.Company.Type != CompanyType.Other")
            //     .OnClick(x => x.Go<Admin.Company.StatusEmailNotificationsUserCustomerPage>()
            //     .Send("company", "info.Company.ID"));


            //ViewModelProperty<Company>("Company").FromRequestParam("company");

        }
    }
}