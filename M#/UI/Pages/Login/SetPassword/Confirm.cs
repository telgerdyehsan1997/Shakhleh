using MSharp;
using Domain;

namespace Login.SetPassword
{
    public class ConfirmPage : SubPage<SetPasswordPage>
    {
        public ConfirmPage()
        {
            Add<Modules.ConfirmPasswordSet>();
        }
    }
}