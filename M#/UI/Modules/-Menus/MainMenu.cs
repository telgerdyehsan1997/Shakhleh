using MSharp;
using Domain;

namespace Modules
{
    public class MainMenu : MenuModule
    {
        public MainMenu()
        {
            AjaxRedirect().IsViewComponent().UlCssClass("nav navbar-nav dropped-submenu");
            //ViewModelProperty<CompanyUser>("CustomerUser");
            //OnBound("loading company user info to know if it's an admin or not").Code(@"
            //        info.CustomerUser = new CompanyUser();
            //        if (User.IsInRole(""Customer""))
            //        {
            //            info.CustomerUser = await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()));
            //        }");

            Item("Login")
                .Icon(FA.UnlockAlt)
                .VisibleIf(AppRole.Anonymous)
                .OnClick(x => x.Go<LoginPage>());

            //Item("Shipments").VisibleIf(AppRole.Admin)
            //    .OnClick(x =>
            //{
            //    x.Go<Admin.ShipmentPage>();
            //});


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