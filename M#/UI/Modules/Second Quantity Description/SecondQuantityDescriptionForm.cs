using MSharp;

namespace Modules
{
    class SecondQuantityDescriptionsForm : FormModule<Domain.SecondQuantityDescription>
    {
        public SecondQuantityDescriptionsForm()
        {
            HeaderText("Second Quantity Description Details");

            Field(x => x.QuantityCode);
            Field(x => x.Description);

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