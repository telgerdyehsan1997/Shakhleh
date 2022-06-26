using MSharp;

namespace Modules
{
    public class LoginForm : FormModule<Domain.User>
    {
        public LoginForm()
        {
            SupportsAdd(false).Using("Olive.Security")
                .SupportsEdit(false)
                .HeaderText("Please Login")
                .DataSource("await Domain.User.FindByEmail(info.Email)");

            Field(x => x.Email).Label("Email").WatermarkText("Your email");
            Field(x => x.Password).Label("Password").Mandatory().WatermarkText("Your password");

            Link("Forgot password").CssClass("text-info float-left").OnClick(x => x.Go<Login.ForgotPasswordPage>().Target(OpenIn.PopupWindow));
            Button("Login").ValidateAntiForgeryToken(false).CssClass("w-20 btn-login mb-2 float-right")
            .OnClick(x =>
            {
                x.RunInTransaction(false);
                x.ShowPleaseWait();
                x.CSharp("info.Item = await Domain.User.FindByEmail(info.Email);");
                x.If("info.Item == null || (info.Item as IArchivable).IsDeactivated").CSharp(@" Notify(""The email or password was incorrect"", ""error""); return View(info); ");
                //x.If("info.Item is CompanyUser").CSharp(@"if(((CompanyUser)info.Item)?.Company.IsOnHold == true){Notify(""Your company is currently on hold"", ""error""); return View(info);}");
                x.CSharp("var isAuthenticated = SecurePassword.Verify(info.Password, info.Item.Password, info.Item.Salt);");

                x.If("!isAuthenticated")
                   .CSharp(@"Notify(""The email or password was incorrect"", ""error""); return View(info);");
                x.CSharp("await info.Item.LogOn(); ");
                //x.CSharp(@"var searchURL = await Database.GetList<SearchUrlCookie>().Where(x => x.UserId == info.Item);
                //            await Database.Delete(searchURL); ");
                x.If(CommonCriterion.RequestHas_ReturnUrl).ReturnToPreviousPage();
                x.Go<Login.DispatchPage>();
            });

            //Reference<ContentBlockView>();
        }
    }
}
