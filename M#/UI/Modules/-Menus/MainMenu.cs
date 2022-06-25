using MSharp;
using Domain;

namespace Modules
{
    public class MainMenu : MenuModule
    {
        public MainMenu()
        {
            AjaxRedirect().IsViewComponent().UlCssClass("nav navbar-nav dropped-submenu");
            ViewModelProperty<CompanyUser>("CustomerUser");
            OnBound("loading company user info to know if it's an admin or not").Code(@"
                    info.CustomerUser = new CompanyUser();
                    if (User.IsInRole(""Customer""))
                    {
                        info.CustomerUser = await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()));
                    }");

            Item("Login")
                .Icon(FA.UnlockAlt)
                .VisibleIf(AppRole.Anonymous)
                .OnClick(x => x.Go<LoginPage>());

            Item("Shipments").VisibleIf(AppRole.Admin)
                .OnClick(x =>
            {
                x.Go<Admin.ShipmentPage>();
            });

            Item("Shipments Into UK").VisibleIf(AppRole.Customer).VisibleIf("CompanyUser.IsEAD")
                .OnClick(x =>
                {
                    x.Go<Customer.ShipmentsIntoUKPage>();
                });

            Item("Shipments Out of UK").VisibleIf(AppRole.Customer).VisibleIf("CompanyUser.IsEAD")
             .OnClick(x =>
             {
                 x.Go<Customer.ShipmentsOutUKPage>();
             });


            Item("GVMS")
                .VisibleIf("CompanyUser?.Company.GVMSId.IsAnyOf(GVMSType.Sometimes, GVMSType.Always) ?? false")
                .OnClick(x => x.Go("https://customspro.net:8112/login", OpenIn.NewBrowserWindow));

            Item("Companies").OnClick(x => x.Go<Admin.CompaniesPage>()).VisibleIf(AppRole.Admin);

            Item("Accounting").OnClick(x => x.Go<Admin.AccountingPage>())
                .VisibleIf("ChannelPortsUser.MobileNumber.HasValue()", AppRole.Admin);

            Item("Invoices").OnClick(x => x.Go<Share.Invoices.CustomerInvoicesPage>().Send("company", "CompanyUser?.CompanyId"))
                .VisibleIf("CompanyUser.AccountsDepartment == true", AppRole.Customer);

            Item("Deposits").OnClick(x => x.Go<Customer.DepositsPage>().Send("company", "CompanyUser?.CompanyId"))
               .VisibleIf(AppRole.Customer);

            Item("Settings")
                    .OnClick(x =>
                    {
                        x.Go<Admin.SettingsPage>().If(AppRole.Admin);
                        x.Go<Customer.SettingsPage>().If(AppRole.Customer);
                    }).VisibleIf(@"info.CustomerUser.IsAdmin == true || User.IsInRole(""Admin"")");

            Item("Dashboard")
                .ItemTemplate(@"@if(info.CustomerUser.IsAdmin == true)
                    { 
                      @if(await Helper.TotalCountUnseenResponse() > 0)
                      {
                         <li class=""@(""active"".OnlyWhen(Model.ActiveItem == ""Dashboard"") + ""active-parent"".OnlyWhen(Model.ActiveItem.OrEmpty().StartsWith(""Dashboard/"")))"">
                           <a name = ""Dashboard"" href = ""#"" formaction = '@Url.ActionWithQuery(""MainMenu/Dashboard"")'>Dashboard</a><span class=""badge"">@await Helper.TotalCountUnseenResponse()</span></li>
                      } 
                      else{
 
                          <li class=""@(""active"".OnlyWhen(Model.ActiveItem == ""Dashboard"") + ""active-parent"".OnlyWhen(Model.ActiveItem.OrEmpty().StartsWith(""Dashboard/"")))"">
                            <a name = ""Dashboard"" href = ""#"" formaction = '@Url.ActionWithQuery(""MainMenu/Dashboard"")'>Dashboard</a></li>
                      }
                    }
                   else{

                        <li class=""@(""active"".OnlyWhen(Model.ActiveItem == ""Dashboard"") + ""active-parent"".OnlyWhen(Model.ActiveItem.OrEmpty().StartsWith(""Dashboard/"")))"">
                         <a name = ""Dashboard"" href = ""#"" formaction = '@Url.ActionWithQuery(""MainMenu/Dashboard"")'>Dashboard</a></li>

                    }")
                 .OnClick(x =>
                 {
                     x.Go<Admin.Dashboard.SupportTicketListPage>().If(AppRole.Admin);
                     x.Go<Admin.Dashboard.CustomerSupportTicketPage>().If(AppRole.Customer);

                 }).VisibleIf(@"(info.CustomerUser.IsAdmin == true || User.IsInRole(""Admin""))");

            Item(@"<img src='~/img/8.png' style='height: 23px;'/>")
                    .OnClick(x =>
                    {
                        x.Go<Customer.BroadcastMessagePage>();

                    }); 

            Item("Logout")
               .VisibleIf(CommonCriterion.IsUserLoggedIn)
               .CssClass("menu-item")
               .OnClick(x =>
               {
                   x.CSharp("await Olive.Security.OAuth.Instance.LogOff();");
                   x.Go<LoginPage>();
               });
        }
    }
}