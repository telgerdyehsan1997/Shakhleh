using MSharp;

namespace Modules
{
    class CurrencyForm : FormModule<Domain.Currency>
    {
        public CurrencyForm()
        {
            HeaderText("Currency Details");

            Field(x => x.Name).Label("Currency");

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
