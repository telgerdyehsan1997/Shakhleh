using MSharp;

namespace Modules
{
    class RecordDetailForm : FormModule<Domain.Company>
    {
        public RecordDetailForm()
        {
            HeaderText("Record Details");
            DataSource("(await Database.FirstOrDefault<CompanyUser>(x => x.ID == Guid.Parse(User.GetId()))).Company");
            ViewModelProperty<Domain.CompanyUser>("user")
                .FromRequestParam("user");

            Field(x => x.Name).Label("Company name").RequiredValidationMessage("The Company name field is required.").AutoFocus(true);

            Field(x => x.Country)
                .Control(ControlType.AutoComplete)
                .DataSource("await Country.GetActiveOrderedCountries()")
                .RequiredValidationMessage("The Country field is required.")
                .ReloadOnChange()
                .DisplayExpression("item.Code +  \" - \" + item.ToString()")
                .ChangeEventHandler("info.IsUKAddress = info.Country?.Code == \"GB\";");

            CustomField("Postcode")
                .VisibleIf("info.IsUKAddress")
                .Mandatory()
                .Label("Postcode/Zip code")
                .ControlMarkup("<div class=\"company-postcode-lookup\"></div>");

            Field(x => x.Postcode)
                .ItemCssClass(@"@(""hidden"".OnlyWhen(info.IsUKAddress))")
                .Label("Postcode/Zip code")
                .RequiredValidationMessage("The Postcode/Zip code field is required.");

            Field(x => x.AddressLine1);
            Field(x => x.AddressLine2);
            Field(x => x.Town);
            Field(x => x.EORINumber).ItemCssClass(@"@(""required-item"".OnlyWhen(info.IsUKAddress))");
            Field(x => x.BranchIdentifier);
            Field(x => x.AEONumber);


            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.If("info.Item.TIN.HasValue() && !info.Item.IsTINNumberValid(info.Item.TIN, info.Country)")
                .MessageBox("Please contact customspro@channelports.co.uk asking for the details to be updated. This will need to be referred to a manger to amend the details to ensure the use of the NCTS guarantee is not impacted.").AndExit();
                // x.If("info.Item.IsNew && info.PaymentType == null").MessageBox("The Deferment Number field is required.").AndExit();
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
            });

            ViewModelProperty<bool>("IsUKAddress");

            OnBound_GET("Set IsUKAddress")
                .Code(@"
                        info.IsUKAddress = info.Item.Country?.Code == ""GB"";
                    ");


            LoadJavascriptModule("scripts/components/company-form.js");
        }
    }
}
