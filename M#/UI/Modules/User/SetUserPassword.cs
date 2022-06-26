using MSharp;
using Domain;

namespace Modules
{
    public class SetUserPassword : FormModule<Domain.User>
    {
        public SetUserPassword()
        {
            SupportsAdd(false)
                .SupportsEdit()
                .HeaderText("Set Your Password")
                .DataSource("info.Ticket.User")
                .SecurityChecks("Request.Has(\"Ticket\")");

            Field(x => x.Password).Mandatory().AfterControl("<div class='password-strength'></div>");
            CustomField().Label("Confirm new password")
                .Mandatory()
                .PropertyName("ConfirmPassword")
                .ExtraControlAttributes("type=\"password\"")
                .ViewModelAttributes("[System.ComponentModel.DataAnnotations.Compare(\"Password\",ErrorMessage=\"New password and Confirm password do not match. Please try again.\")]")
                .Control(ControlType.Textbox);

            ViewModelProperty<PasswordResetTicket>("Ticket").FromRequestParam("ticket");

            Button("Cancel").OnClick(x => x.Go<LoginPage>());

            Button("Save").Icon(FA.Check).IsDefault()
            .OnClick(x =>
            {
                //x.If("info.Ticket.IsExpired || info.Ticket.IsUsed")
                //.MessageBox("This ticket is no longer valid. Please request a new ticket.")
                //.AndExit();

                x.CSharp("await PasswordResetService.Complete(info.Ticket, info.Password.Trim());");
                x.Go<Login.SetPassword.ConfirmPage>().Send("item", "info.Ticket.UserId");
            });
        }
    }
}