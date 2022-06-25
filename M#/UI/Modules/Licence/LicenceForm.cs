using MSharp;

namespace Modules
{
    class LicenceForm : FormModule<Domain.Licence>
    {
        public LicenceForm()
        {
            HeaderText("Licence details");

            Field(x => x.Type)
                .AsRadioButtons(Arrange.Horizontal)
                .ReloadOnChange();

            Field(x => x.LicenceName);

            Field(x => x.LicenceType)
                .AsRadioButtons(Arrange.Horizontal);

            Field(x => x.LicenceIdentifier);

            Field(x => x.ChiefLicenceCode)
                .Mandatory();

            Field(x => x.LicenceStatusCode)
                .AsDropDown()
                .DataSource(@"await Database.Of<LicenceStatusCode>().Where(x => !x.IsDeactivated && (x.Type == info.Type || x.IsForShipmentsInAndOutOfUK == true)).GetList()")
                .Label("Licence status code")
                .Mandatory();

            Field(x => x.Quantity)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal);

            Field(x => x.RPTID)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal);

            Button("Cancel")
                .OnClick(x => x.ReturnToPreviousPage());

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