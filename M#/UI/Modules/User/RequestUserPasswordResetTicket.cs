using MSharp;
using Domain;

namespace Modules
{
    public class RequestUserPasswordResetTicket : FormModule<Domain.User>
    {
        public RequestUserPasswordResetTicket()
        {
            IsViewComponent();
            SupportsAdd(false).SupportsEdit(false)
                .Header("<p> Please enter your email address below and press Send button. We will send you an email with a link to reset your password: </p>")
                .HeaderText("Forgot password");
            RootCssClass("forgot-password-form");

            Field(x => x.Email);

            Button("Cancel").OnClick(x => x.CloseModal());
            Button("Send").IsDefault()
            .OnClick(x =>
            {
                x.CSharp("var user = await Domain.User.FindByEmail(info.Email.Trim());");
                x.If("user == null")
                    .MessageBox("Invalid email address. Please try again.")
                    .AndExit();
                x.CSharp("await PasswordResetService.RequestTicket(user);");
                x.GentleMessage("Change Password instructions have been sent to your email address.");
                x.CloseModal();
                //x.Display(@"<h2> Forgot Your Password </h2>
                //<p> An email containing instructions to change your password has been sent to your email address. </p>").IsHtml();
            });
        }
    }
}