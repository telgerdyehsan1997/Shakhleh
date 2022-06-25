using MSharp;

namespace Modules
{
    class ExchangeRateEnter : FormModule<Domain.ExchangeRate>
    {
        public ExchangeRateEnter()
        {
            HeaderText("Update Rate");

            Field(x => x.Rate).Mandatory();

            Field(x => x.Currency)
                .AsAutoComplete()
                .Mandatory();

            Field(x => x.UpdatedDate).Label("Date Last Updated").Readonly();

            Button("Cancel")
                 .OnClick(x =>
                 {
                     x.CloseModal();
                 });

            Button("Save")
                .OnClick(x =>
                {
                    x.RunInTransaction();
                    x.SaveInDatabase();
                    x.GentleMessage("Saved successfully.");
                    x.ReturnToPreviousPage();
                });
            OnBeforeSave("save latest value").Code("item.UpdatedDate = LocalTime.Now;");
        }
    }
}