using MSharp;

namespace Modules
{
    class ShipmentContactForm : FormModule<Domain.Contact>
    {
        public ShipmentContactForm()
        {
            ViewModelProperty<Domain.Company>("Company").FromRequestParam("company");
            AutoSet(x => x.Company);

            HeaderText("New Contact");
            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email).Label("Email address");
            Field(x => x.TelephoneNumber);
            Field(x => x.MobileNumber);

            Button("Cancel").OnClick(x => x.CloseModal());
            Button("Save").IsDefault().Icon(FA.Check)
                .OnClick(x =>
                {
                    x.SaveInDatabase();
                    x.CSharp(@"SaveInSession(info.Item.ID.ToString() ,""primary"");");
                    x.GentleMessage("Saved successfully.");
                    x.CloseModal(Refresh.Ajax);
                });
        }
    }
}