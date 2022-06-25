using MSharp;

namespace Modules
{
    class CPCDetails : FormModule<Domain.CPC>
    {
        public CPCDetails()
        {
            HeaderText("CPC Details");

            Field(x => x.Type).Control(ControlType.HorizontalRadioButtons).Mandatory();
            Field(x => x.Number);
            Field(x => x.CPCDescription);
            Field(x => x.Box44);
            Field(x => x.Manual)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.OverrideEORI)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.EORI);
            Field(x => x.NoTaxLine)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons);

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