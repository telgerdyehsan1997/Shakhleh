using MSharp;

namespace Modules
{
    class CompanyUserDetails : FormModule<Domain.CompanyUser>
    {
        public CompanyUserDetails()
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
                .Label("Customer Admin")
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.RecievesCFSPReport)
             .Label("Recieves CFSP Report")
             .Mandatory()
             .Control(ControlType.HorizontalRadioButtons);

            AutoSet(x => x.Company).Value("info.Company");

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });
        }
    }
}