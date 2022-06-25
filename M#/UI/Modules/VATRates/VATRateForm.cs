using MSharp;

namespace Modules
{
    class VATRateForm : MFABaseForm<Domain.VATRate>
    {
        public VATRateForm()
        {
            HeaderText("VAT Rate Details");

            Field(x => x.ValidFrom);
            Field(x => x.Name).Label("VAT Rate Name");
            Field(x => x.RateS).Label("VAT Rate for S");
            Field(x => x.RateZ).Label("VAT Rate for Z");
            Field(x => x.RateA).Label("VAT Rate for A");
            Field(x => x.Statement);

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