using MSharp;
using Domain;

namespace Modules
{
    public class Header : GenericModule
    {
        public Header()
        {
            IsInUse().IsViewComponent().WrapInForm(false);
            WrapInForm();
            Using("Olive.Security");
            RootCssClass("header-wrapper");
            var logo = Image("Logo").CssClass("logo").ImageUrl("~/img/Logo.jpg")
                  .OnClick(x => x.Go("~/"));

            var menu = Reference<AdminMainMenu>();
            var customerMenu = Reference<ShopUserMainMenu>();


            var login = Link("ورود").Icon(FA.UnlockAlt)
                        .VisibleIf(AppRole.Anonymous)
                        .OnClick(x => x.Go<LoginPage>());

            var logout = Link("خروج")
                         .CssClass("align-bottom logout")
                         .ValidateAntiForgeryToken(false)
                         .VisibleIf(CommonCriterion.IsUserLoggedIn)
                         .OnClick(x =>
                         {
                             x.CSharp("await OAuth.Instance.LogOff();");
                             x.Go<LoginPage>();
                         });
            Markup($@"
            <nav class=""navbar"">
              <div class=""header-left-actions-wrapper"">
                      {logo.Ref}
              </div>
            <div class="""">
                @if (User.IsInRole(""Admin""))
         @(await Component.InvokeAsync
         <AdminMainMenu>
            ())
            else if (User.IsInRole(""ShopUser""))
            @(await Component.InvokeAsync
            <ShopUserMainMenu>
               ())
               
              </div>
              <div class=""header-account-wrapper"">
                    {logout.Ref}
                  </div>
            </nav>");

        }
    }
}