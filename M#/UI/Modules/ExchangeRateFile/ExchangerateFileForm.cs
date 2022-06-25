using MSharp;

namespace Modules
{
    class ExchangerateFileForm : FormModule<Domain.ExchangeRateFile>
    {
        public ExchangerateFileForm()
        {
            HeaderText("Exchange Rate Details");

            CustomField("ExchangeUrl").Label("").ItemCssClass("readonly-field").ControlMarkup(@"<a href='@AppSetting.ExchangeUrl' target='_parent'>@AppSetting.ExchangeUrl</a>");

            Field(x => x.Name);
            Field(x => x.File);

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.Go<Admin.Settings.ExchangeRateFilePage>();
            });
        }
    }
}