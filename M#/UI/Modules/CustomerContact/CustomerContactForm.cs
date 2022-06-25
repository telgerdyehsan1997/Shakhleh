using MSharp;

namespace Modules
{
    class CustomerContactForm : FormModule<Domain.Contact>
    {
        public CustomerContactForm()
        {
            // TODO: Configure me ...!
            HeaderText("Contact details");

            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email).Label("Email address");
            Field(x => x.TelephoneNumber);
            Field(x => x.MobileNumber);
            Field(x => x.Notes);

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