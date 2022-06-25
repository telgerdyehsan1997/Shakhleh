using Domain;
using MSharp;

namespace Modules
{
    public class Footer : GenericModule
    {
        const string DEVELOPER = "http://www.geeks.ltd.uk";

        public Footer()
        {
            IsInUse().IsViewComponent()
                .Using("Olive.Security")
                .RootCssClass("website-footer")
                .Markup(@"
           <div class=""pull-right"">
               [#BUTTONS(SoftwareDevelopment)#] by [#BUTTONS(Geeks)#]            
            </div>            
            <div>
                &copy; @LocalTime.Now.Year. All rights reserved.
            </div>");

            //<div>[#BUTTONS(Logout)#]</div>

            //Link("Logout")
            //    .ValidateAntiForgeryToken(false)
            //    .Icon(FA.SignOut)
            //    .MarkupTemplate("Hi @GetUser() ([#Button#])")
            //    .VisibleIf(CommonCriterion.IsUserLoggedIn)
            //    .OnClick(x =>
            //    {
            //        x.CSharp("await OAuth.Instance.LogOff();");
            //        x.Go<LoginPage>();
            //    });

            Link("Geeks")
                .OnClick(x => x.Go(DEVELOPER, OpenIn.NewBrowserWindow));

            Link("Software development")
                .CssClass("plain-text")
                .OnClick(x => x.Go(DEVELOPER, OpenIn.NewBrowserWindow));
        }
    }
}