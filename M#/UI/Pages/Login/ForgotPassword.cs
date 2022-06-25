using MSharp;
using Domain;

namespace Login
{
    public class ForgotPasswordPage : SubPage<LoginPage>
    {
        public ForgotPasswordPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.RequestUserPasswordResetTicket>();
        }
    }
}