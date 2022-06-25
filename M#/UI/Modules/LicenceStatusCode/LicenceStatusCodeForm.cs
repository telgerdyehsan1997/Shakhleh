using MSharp;

namespace Modules
{
    class LicenceStatusCodeForm : FormModule<Domain.LicenceStatusCode>
    {
        public LicenceStatusCodeForm()
        {
            HeaderText("Status Code Cetails");

            Field(x => x.StatusCode);
            Field(x => x.Type)
                .AsRadioButtons(Arrange.Horizontal);

            Field(x => x.LicenceType)
                .AsRadioButtons(Arrange.Horizontal);

            Field(x => x.Description);
            Field(x => x.IsForShipmentsInAndOutOfUK).ItemCssClass("d-none");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp(@"info.IsForShipmentsInAndOutOfUK = info.Type == null;");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            OnPostBound("adding both option").Code(@"
            var nullOption = info.Type_Options.FirstOrDefault(x => x.Text == ""N/A"");
            if (nullOption != null)
            {
                nullOption.Text = ""Both"";
            }
            ");


        }
    }
}