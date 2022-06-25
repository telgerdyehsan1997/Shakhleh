using MSharp;
using Domain;

namespace Modules
{
    public class CustomerUserForm : FormModule<Domain.CompanyUser>
    {
        public CustomerUserForm()
        {
            HeaderText("Company User Details");

            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email).Label("Email address");
            Field(x => x.TelephoneNumber);
            Field(x => x.MobileNumber);
            Field(x => x.Notes);

            Field(x => x.AccountsDepartment)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.IsAdmin)
                .Mandatory()
                .Label("Customer Admin")
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.RecievesCFSPReport)
              .Mandatory()
              .Label("Recieves CFSP Report")
              .Control(ControlType.HorizontalRadioButtons);

            AutoSet(x => x.Company).Value("info.Company");

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.RunInTransaction(false);
                x.SaveInDatabase();
                x.CSharp(@"if (!((info.IsAdmin == true || User.IsInRole(""Admin""))))
                             return new UnauthorizedResult(); ");
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });
        }
    }
}