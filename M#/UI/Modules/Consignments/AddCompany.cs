using MSharp;

namespace Modules
{
    class AddCompany : FormModule<Domain.Company>
    {
        public AddCompany()
        {
            HeaderText("Add Company").RootCssClass("add-company");
            Inject("APIHandler.IEORIService");
            AutoSet(x => x.Type).Value("CompanyType.Other");

            Field(x => x.Name).Label("Company name").RequiredValidationMessage("The Company name field is required.").AutoFocus(true);
            Field(x => x.Country)
                .Control(ControlType.AutoComplete)
                .SourceCriteria("!item.IsDeactivated")
                .ReloadOnChange()
                .ChangeEventHandler("info.IsUKAddress = info.Country?.Code == \"GB\";")
                .RequiredValidationMessage("The Country field is required.");

            CustomField("Postcode").VisibleIf("info.IsUKAddress").Label("Postcode/Zip code").ControlMarkup("<div class=\"company-postcode-lookup\"></div>");
            Field(x => x.Postcode).ItemCssClass(@"@(""hidden"".OnlyWhen(info.IsUKAddress))").Label("Postcode/Zip code").RequiredValidationMessage("The Postcode/Zip code field is required.");

            Field(x => x.AddressLine1);
            Field(x => x.AddressLine2);
            Field(x => x.Town);
            Field(x => x.EORINumber).ItemCssClass(@"@(""required-item"".OnlyWhen(info.Country?.Code == ""GB"" || info.IsTrader || info.FieldName==""uktrader""))");
            Field(x => x.PaymentType).AsDropDown().VisibleIf("!info.IsPartner");
            Field(x => x.DefermentNumber).VisibleIf("!info.IsPartner");
            Field(x => x.RepresentationType).VisibleIf("!info.IsPartner");

            AutoSet(x => x.RepresentationType).OnlyIf("info.IsPartner").Value("true");
            Button("Cancel").OnClick(x => x.CloseModal());

            Button("Save").IsDefault().Icon(FA.Check)
                .OnClick(x =>
                {
                    x.If("!info.IsPartner && !info.EORINumber?.StartsWith(\"GB\") == true")
                       .MessageBox("EORINumber must start with GB.").AndExit();

                    x.If("info.IsTrader && info.EORINumber == Constants.ChannelPortEORI")
                    .MessageBox("You can't use ChannelPorts EORI for UKTrader.").AndExit();

                    x.ElseIf("info.IsPartner && info.Country == await Country.GetUK()")
                        .MessageBox("A partner company must have a non-UK country.").AndExit();

                    x.CSharp(@"var isValidEORI = await info.Item.IsEORINumberValid(info.EORINumber, info.Country);");
                    x.If(@"!isValidEORI && info.EORINumber.HasValue()").MessageBox("The EORI number is invalid.").AndExit();

                    x.CSharp(@"var isGBValidEORI = await IEORIService.IsGBEoriNumberValidate(info.EORINumber);");
                    x.If(@"!isGBValidEORI && info.IsTrader").MessageBox("The EORI number is invalid.").AndExit();



                    x.CSharp("var company = await Company.FindByDefermentNumberAndEORINumberAndNameAndPostcodeAndTown(info.DefermentNumber, info.EORINumber, info.Name, info.Postcode, info.Town);");
                    x.If("company != null", AppRole.Customer).CSharp(@"
                            await (Context.Current.User().ExtractUser<User>() as CompanyUser).Company.AddCompanyToAssociatedCompanies(company);
                            info.Item = company;
                    ");
                    x.Else().SaveInDatabase();
                    x.CSharp(@"SaveInSession(info.Item.ID.ToString(), info.FieldName);");
                    x.GentleMessage("Saved successfully.");
                    x.CloseModal(Refresh.Ajax);
                });

            ViewModelProperty<string>("FieldName").FromRequestParam("fieldname");

            ViewModelProperty<bool>("IsUKAddress");
            ViewModelProperty<bool>("IsPartner").FromRequestParam("isPartner");
            ViewModelProperty<bool>("IsTrader").FromRequestParam("isTrader");
            OnBound_GET("Set IsUKAddress").Code(@"info.IsUKAddress = info.Item.Country?.Code == ""GB"";");



            LoadJavascriptModule("scripts/components/company-form.js");
            LoadJavascriptModule("scripts/components/company-autoset.js");


        }
    }
}