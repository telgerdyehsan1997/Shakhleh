using MSharp;

namespace Modules
{
    class CompanyAPISettingsForm : FormModule<Domain.Company>
    {
        public CompanyAPISettingsForm()
        {
            RequestParam("company");
            HeaderText("API Settings");
            SupportsAdd(false);

            Field(x => x.Username).Mandatory();
            Field(x => x.Password).Mandatory();
            Field(x => x.PrimaryContact).Mandatory().Control(ControlType.AutoComplete).DataSource("await info.Item.GetAvailableContacts()");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.RefreshPage();
            });
        }
    }
}