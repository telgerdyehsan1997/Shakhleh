using MSharp;
using Domain;

namespace Login
{
    public class SetPasswordPage : SubPage<LoginPage>
    {
        public SetPasswordPage()
        {
            Route("password/set/{ticket}");

            Add<Modules.SetUserPassword>();
        }
    }
}