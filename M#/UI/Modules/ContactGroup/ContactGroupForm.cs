using MSharp;

namespace Modules
{
    class ContactGroupForm : FormModule<Domain.ContactGroup>
    {
        public ContactGroupForm()
        {
            // TODO: Configure me ...!
            HeaderText("Contact Group Details");

            Field(x => x.GroupName);
            AutoSet(x => x.Company).Value("info.Company");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");

        }
    }
}