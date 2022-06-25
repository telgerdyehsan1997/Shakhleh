using MSharp;

namespace Modules
{
    class TransitOfficeForm : FormModule<Domain.TransitOffice>
    {
        public TransitOfficeForm()
        {

            HeaderText("Office of Transit details");

            Field(x => x.CountryCode);
            Field(x => x.CountryName);
            Field(x => x.NCTSCode);
            Field(x => x.UsualName);
            Field(x => x.Departure)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal);
            Field(x => x.Destination)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal);
            Field(x => x.Transit)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal);

            Field(x => x.NearestOffice);
            //Field(x => x.GeoLocation);

            MasterDetail(x => x.TransitOfficeAlias, a =>
            {
                a.Field(x => x.Alias).NoLabel().
                CustomDataSave("info.TransitOfficeAlias = info.TransitOfficeAlias.ToList();");
                a.Button("Add Alias").CausesValidation(false)
               .CssClass("add-button").OnClick(x => x.AddMasterDetailRow());
            }).Label("Alias");


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