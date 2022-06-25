using MSharp;

namespace Modules
{
    class AuthorisedLocationsForm : FormModule<Domain.AuthorisedLocation>
    {
        public AuthorisedLocationsForm()
        {
            HeaderText("Authorised Location Details");

            OnPostBound("Change transit office text to NCTS code")
                .Code("info.TransitOffice_Text = info.TransitOffice?.NCTSCode.ToStringOrEmpty();")
                .Criteria("Request.IsGet()");

            Field(x => x.LocationName);
            Field(x => x.CustomsIdentity);
            Field(x => x.TransitOffice).Label("NCTS code").AsAutoComplete().DisplayExpression("item.NCTSCode")
                .DataSource("await TransitOffice.Departures").RequiredValidationMessage("The NCTS code field is required.");
            Field(x => x.AuthorisationNumber);
            Field(x => x.EmailAddresses);

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