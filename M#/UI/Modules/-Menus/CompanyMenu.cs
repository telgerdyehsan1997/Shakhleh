using MSharp;
using Domain;

namespace Modules
{
    public class CompanyMenu : MenuModule
    {
        public CompanyMenu()
        {
            IsViewComponent().RootCssClass("navbar navbar-light").UlCssClass("nav flex-column w-100");

            Item("Accounting Information")
                .VisibleIf("ChannelPortsUser.MobileNumber.HasValue()")
                .OnClick(x => x.Go<Admin.Company.AccountingInformationPage>()
                .Send("company", "info.Company.ID"));

            Item("Ancillaries")
               .VisibleIf(AppRole.SuperAdmin)
               .OnClick(x => x.Go<Admin.Company.AncillariesPage>()
               .Send("company", "info.Company.ID"));

            Item("API Settings")
               .OnClick(x => x.Go<Admin.Company.APISettingsPage>()
               .Send("company", "info.Company.ID"));

            Item("Company Users")
                .VisibleIf("info.Company.Type != CompanyType.Other")
                .OnClick(x => x.Go<Admin.Company.CompanyUsersPage>()
                .Send("company", "info.Company.ID"));

            Item("Contacts")
                .VisibleIf("info.Company.Type != CompanyType.Other")
                .OnClick(x => x.Go<Admin.Company.ContactsPage>()
                .Send("company", "info.Company.ID"));

            Item("Contact Groups")
                .VisibleIf("info.Company.Type != CompanyType.Other")
                .OnClick(x => x.Go<Admin.Company.ContactGroupsPage>()
                .Send("company", "info.Company.ID"));

            Item("Custom Licenses")
                .VisibleIf("ChannelPortsUser.MobileNumber.HasValue()")
                .OnClick(x => x.Go<Admin.Company.ChargesPage>()
                .Send("company", "info.Company.ID"));

            Item("Details")
             .OnClick(x => x.Go<Admin.Company.DetailsPage>()
             .Send("company", "info.Company.ID"));

            Item("Deposits")
               .OnClick(x => x.Go<Admin.Company.DepositsPage>()
               .Send("company", "info.Company.ID"));

            Item("Invoices")
               .OnClick(x => x.Go<Admin.Company.InvoicesPage>()
               .Send("company", "info.Company.ID"));

            Item("Notes")
                .OnClick(x => x.Go<Admin.Company.NotePage>()
                .Send("company", "info.Company.ID"));

            Item("Products")
                .OnClick(x => x.Go<Admin.Company.ProductsPage>()
                .Send("company", "info.Company.ID"));

            Item("Special CPCs")
              .OnClick(x => x.Go<Admin.Company.CompanyCPCPage>()
              .Send("company", "info.Company.ID"));

            Item("UK Traders/ Partners")
               .OnClick(x => x.Go<Admin.Company.UKTradersPartnersPage>()
               .Send("company", "info.Company.ID"));


            Item("Status Email Notifications")
                  .VisibleIf("info.Company.Type != CompanyType.Other")
                 .OnClick(x => x.Go<Admin.Company.StatusEmailNotificationsUserCustomerPage>()
                 .Send("company", "info.Company.ID"));


            ViewModelProperty<Company>("Company").FromRequestParam("company");

        }
    }
}