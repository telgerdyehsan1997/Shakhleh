using MSharp;

namespace Modules
{
    class AddCarrierForm : FormModule<Domain.Carrier>
    {
        public AddCarrierForm()
        {
            HeaderText("Carrier Details");

            Field(x => x.Name).Label("Carrier name")
                .AutoFocus(true);

            Field(x => x.Country)
                .Control(ControlType.AutoComplete)
                .DataSource("await Country.GetActiveOrderedCountries()")
                .RequiredValidationMessage("The Country field is required.")
                .ReloadOnChange()
                .DisplayExpression(@"item.Code +  "" - "" + item.ToString()")
                .ChangeEventHandler(@"info.IsUKAddress = info.Country?.Code == ""GB"";");

            CustomField("Postcode")
                .VisibleIf("info.IsUKAddress")
                .Mandatory()
                .Label("Postcode/Zip code")
                .ControlMarkup(@"<div class=""company-postcode-lookup""></div>");

            Field(x => x.Postcode)
                .ItemCssClass(@"@(""hidden"".OnlyWhen(info.IsUKAddress))")
                .Label("Postcode/Zip code")
                .RequiredValidationMessage("The Postcode/Zip code field is required.");

            Field(x => x.AddressLine1);

            Field(x => x.AddressLine2);

            Field(x => x.Town);

            Field(x => x.EORINumber).ItemCssClass(@"@(""required-item"".OnlyWhen(info.IsUKAddress))");

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.CSharp(@"SaveInSession(info.Item.ID.ToString() ,""carrier"");");
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Ajax);
            });

            ViewModelProperty<bool>("IsUKAddress");

            OnBound("Set IsUKAddress")
                .Code(@"
                        info.IsUKAddress = info.Item.Country?.Code == ""GB"";
                ");

            LoadJavascriptModule("scripts/components/company-form.js");
        }
    }
}
