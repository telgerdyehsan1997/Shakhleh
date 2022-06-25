using MSharp;

namespace Modules
{
    class AncillaryForm : FormModule<Domain.Ancillary>
    {
        public AncillaryForm()
        {
            HeaderText("Freight Charge Details");

            CustomField("").Label("Country").PropertyName("Country.Code").Readonly();
            Field(x => x.FreightChargePerTonne).Label("Freight charge per tonne");
            Field(x => x.FullLoadFreightCharge).Label("Full load freight charge");
            Field(x => x.ValueForVAT);

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
