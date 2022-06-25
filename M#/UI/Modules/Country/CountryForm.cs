using MSharp;

namespace Modules
{
    class CountryForm : FormModule<Domain.Country>
    {
        public CountryForm()
        {
            HeaderText("Country Details");
            Field(x => x.Name).Label("Country");
            Field(x => x.Code).Label("Country code");
            Field(x => x.EU27)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons)
                .ReloadOnChange();
            Field(x => x.PreferenceAvailable)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons)
                .ReloadOnChange()
                .CustomDataSave(@"if(info.PreferenceAvailable.HasValue && info.PreferenceAvailable == false){
                    item.ImportCPCWithPreference = null;
                    item.ImportCPCWithPreferenceDeclarationType = """";
                    item.ImportCPCWithPreferencePreferenceCode = """";
                    item.ExportCPCWithPreference = null;
                    item.ExportCPCWithPreferenceDeclarationType = """";
                    }");

            Field(x => x.HasMFN)
                .Label("MFN")
                .Mandatory()
                .ReloadOnChange()
                .Control(ControlType.HorizontalRadioButtons)
                .CustomDataSave(@"if(info.HasMFN == false){
                    item.MFNCode1 = """";
                    item.MFNCode2 = """";
                    item.MFNCode3 = """";
                    item.MFNCode4 = """";
                    item.MFNCode5 = """";
                    }");

            Field(x => x.MFNCode1).Mandatory().VisibleIf("info.HasMFN == true");
            Field(x => x.MFNCode2).VisibleIf("info.HasMFN == true");
            Field(x => x.MFNCode3).VisibleIf("info.HasMFN == true");
            Field(x => x.MFNCode4).VisibleIf("info.HasMFN == true");
            Field(x => x.MFNCode5).VisibleIf("info.HasMFN == true");

            Field(x => x.ImportCPCWithPreference).Control(ControlType.AutoComplete)
                  .SourceCriteria("!item.IsDeactivated")
                .Mandatory().VisibleIf("info.PreferenceAvailable  == true");
            Field(x => x.ImportCPCWithPreferenceDeclarationType).Mandatory().VisibleIf("info.PreferenceAvailable == true");
            Field(x => x.ImportCPCWithPreferencePreferenceCode).Mandatory().VisibleIf("info.PreferenceAvailable  == true");
            Field(x => x.ImportCPCWithPreferenceRateCode).VisibleIf("info.PreferenceAvailable  == true");
            Field(x => x.ImportCPCWithoutPreference).SourceCriteria("!item.IsDeactivated")
                .Control(ControlType.AutoComplete);
            Field(x => x.ImportCPCWithoutPreferenceDeclarationType).Mandatory();
            Field(x => x.ImportCPCWithoutPreferencePreferenceCode).Mandatory();
            Field(x => x.ImportCPCWithoutPreferenceRateCode);
            Field(x => x.ExportCPCWithPreference).Control(ControlType.AutoComplete).Mandatory().VisibleIf("info.PreferenceAvailable  == true").SourceCriteria("!item.IsDeactivated");
            Field(x => x.ExportCPCWithPreferenceDeclarationType).Mandatory().VisibleIf("info.PreferenceAvailable  == true");
            Field(x => x.ExportCPCWithoutPreference).Control(ControlType.AutoComplete)
                .SourceCriteria("!item.IsDeactivated");
            Field(x => x.ExportCPCWithoutPreferenceDeclarationType).Mandatory();

            Field(x => x.InvoiceDeclarationDocumentType).Mandatory();
            Field(x => x.InvoiceDeclarationDocumentTypeDocumentStatus).Mandatory();
            Field(x => x.PreferenceCertificateNumberDocumentType).Mandatory();
            Field(x => x.PreferenceCertificateNumberDocumentTypeDocumentStatus).Mandatory();
            Field(x => x.CountryDiallingCode);

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