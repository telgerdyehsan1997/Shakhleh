using MSharp;
using Domain;

public class LoginPage : RootPage
{
    public LoginPage()
    {
        Route(@"login
            [#EMPTY#]");

        Layout(Layouts.Blank);

        Add<Modules.LoginForm>();

        MarkupTemplate("<div class=\"login-content\"><div class=\"card login w-50\"><div class=\"card-body\">[#1#]</div></div></div>");

        OnStart(x =>
        {
            x.If("Request.IsAjaxPost()").CSharp("return Redirect(Url.CurrentUri().OriginalString);");
            x.If("User.Identity.IsAuthenticated").Go<Login.DispatchPage>().RunServerSide();
        });
    }
}
