using MSharp;

namespace Modules
{
    class TermsOfSaleForm : FormModule<Domain.TermOfSale>
    {
        public TermsOfSaleForm()
        {
            // TODO: Configure me ...!
            HeaderText("Terms of Sale Details");

            Field(x => x.Name).Label("Terms of sale").RequiredValidationMessage("The Terms of sale field is required");
            Field(x => x.Description);
            Field(x => x.Box45).Control(ControlType.HorizontalRadioButtons);
            Field(x => x.FreightCharge).Control(ControlType.HorizontalRadioButtons);
            Field(x => x.ValueForVAT);
            Field(x => x.IsDDP)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons);
            //Field(x => x.DefaultTCPMImport).Control(ControlType.HorizontalRadioButtons);
            //Field(x => x.DefaultTCPMExport).Control(ControlType.HorizontalRadioButtons);

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