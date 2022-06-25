using MSharp;

namespace Modules
{
    class DefaultLicenseForm : MFABaseForm<Domain.Charge>
    {
        public DefaultLicenseForm()
        {
            HeaderText("Default License Details");

            AutoSet(x => x.IsDefault).Value("true");
            Field(x => x.Name);
            Field(x => x.Currency)
                .AsRadioButtons(Arrange.Horizontal);
            Field(x => x.LicenseFee);
            Field(x => x.IsYearly)
                .AsRadioButtons(Arrange.Horizontal)
                .Label("Free consignments frequency");

            Field(x => x.FreeConsignments)
                .Label("Number of Free Consignments");
            Field(x => x.PricePerAdditionalConsignment);
            Field(x => x.PricePerCommodity);
            
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