using MSharp;

namespace Modules
{
    class DefaultTaxLineForm : FormModule<Domain.DefaultTaxLine>
    {
        public DefaultTaxLineForm()
        {
            HeaderText("Default Tax Line Details");

            Field(x => x.TaxType)
                .AsRadioButtons(Arrange.Horizontal)
                .ItemsSource(@"new string[] { ""A"", ""B"" }")
                .Mandatory();
            Field(x => x.TaxTypeSuffix);
            Field(x => x.BaseAmount);
            Field(x => x.BaseQuantity);
            Field(x => x.Rate);
            Field(x => x.Override);
            Field(x => x.Amount);
            Field(x => x.MoP);

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });
        }
    }
}