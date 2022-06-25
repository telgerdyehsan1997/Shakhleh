using MSharp;

namespace Modules
{
    class CompanyForm : FormModule<Domain.Company>
    {
        public CompanyForm()
        {
            HeaderText("Record Details");

            Field(x => x.Type).Label("Type").Control(ControlType.HorizontalRadioButtons).VisibleIf(AppRole.SuperAdmin)
                .CustomDataSave(@"if (info.Item.IsNew && !(User.IsInRole(""Super Admin"")))
                item.Type = CompanyType.Other;").ReloadOnChange();

            Field(x => x.TransactionTypes)
                .Label("Transaction type(s)")
                .ReloadOnChange()
                .Control(ControlType.HorizontalCheckBoxes);
               // .ItemCssClass(@"@(info.Type == CompanyType.Other || info.GVMS != GVMSType.NotGVMS ? """" : ""required-item"")");

            Field(x => x.GVMS)
               .CustomInitializer(@"
                    info.GVMS_Options.Clear();
                    info.GVMS_Options.AddRange(await Database.GetList<GVMSType>());")
              // .VisibleIf("info.TransactionTypes.Contains(ShipmentType.OutOfUk) || await info.Item.TransactionTypes.Any(x => x.ShipmentType == ShipmentType.OutOfUk)")
               .Control(ControlType.HorizontalRadioButtons)
               .ReloadOnChange()
               .ItemCssClass(@"@(info.Type == CompanyType.Other ? """" : ""required-item"")");

            Field(x => x.SafetyAndSecurityInbound)
               .CustomInitializer(@"
                    info.SafetyAndSecurityInbound_Options.Clear();
                    info.SafetyAndSecurityInbound_Options.AddRange(await Database.GetList<SafetyAndSecurity>(), item => item.DisplayName);")
               .Control(ControlType.HorizontalRadioButtons)
                .CustomDataLoad(@"if (info.Item.IsNew)
                info.SafetyAndSecurityInbound = SafetyAndSecurity.NoSafetyAndSecurity;");

            Field(x => x.SafetyAndSecurityOutbound)
               .CustomInitializer(@"
                    info.SafetyAndSecurityOutbound_Options.Clear();
                    info.SafetyAndSecurityOutbound_Options.AddRange(await Database.GetList<SafetyAndSecurity>(), item => item.DisplayName);")
               .Control(ControlType.HorizontalRadioButtons)
               .CustomDataLoad(@"if (info.Item.IsNew)
                info.SafetyAndSecurityOutbound = SafetyAndSecurity.Always;");

            Field(x => x.CustomerAccountNumber).Mandatory().VisibleIf("info.Type != CompanyType.Other", AppRole.SuperAdmin);
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

            //Field(x => x.TSP);
            //Field(x => x.CFSP)
            //    .VisibleIf("(info.Type == CompanyType.Customer && (info.TransactionTypes.Contains(ShipmentType.IntoUk) || await info.Item.TransactionTypes.Any(x => x.ShipmentType == ShipmentType.IntoUk))) || info.Type == CompanyType.Forwarder || info.Country == await Country.GetUK()")
            //    .Mandatory()
            //    .ReloadOnChange()
            //    .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.CFSPType)
                .Label("CFSP")
                .VisibleIf("(info.Type.IsAnyOf(CompanyType.Customer, CompanyType.Flex) && (info.TransactionTypes.Contains(ShipmentType.IntoUk) || await info.Item.TransactionTypes.Any(x => x.ShipmentType == ShipmentType.IntoUk))) || info.Type == CompanyType.Forwarder || info.Country == await Country.GetUK()")
                .Mandatory()
                .ReloadOnChange()
                .ChangeEventHandler(@"if(info.CFSPType == CFSPType.Channelports)
                                    {
                                        var channelPorts = await Company.ChannelPort;
                                        info.AuthorisedCompanyName = channelPorts.Name;
                                        info.AuthorisedAddressLine1 = channelPorts.AddressLine1;
                                        info.AuthorisedAddressLine2 = channelPorts.AddressLine2;
                                        info.AuthorisedPostcode = channelPorts.Postcode;
                                        info.AuthorisedTownOrCity = channelPorts.Town;
                                        info.Country = channelPorts.Country;
                                    }")
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.UsingEIDR)
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports)")
                .Label("Using EIDR?")
                .Control(ControlType.HorizontalRadioButtons);

            //Field(x => x.ChannelportsCFSP)
            //    .Control(ControlType.HorizontalRadioButtons)
            //    .Mandatory()
            //    .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports)")
            //    .ReloadOnChange();

            Field(x => x.AuthorisationNumber)
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own)")
                .ReloadOnChange();

            CustomField().ControlMarkup("@AppSetting.CFSPChannelportsAuthorisationNumber").Label("Authorisation Number").VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)");

            CustomField()
                .ControlMarkup("<h3>Authorised Company Details</h3>")
                .NoLabel()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)");

            Field(x => x.AuthorisedCompanyName)
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)");

            Field(x => x.AuthorisedPostcode)
                .Label("Postcode")
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)");

            Field(x => x.AuthorisedAddressLine1)
                .Label("Address Line 1")
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)");

            Field(x => x.AuthorisedAddressLine2)
                .Label("Address Line 2")
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)");

            Field(x => x.AuthorisedTownOrCity)
                .Label("Town/ City")
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)");

            Field(x => x.AuthorisedCountry)
                .Label("Country")
                .Control(ControlType.AutoComplete)
                .DataSource("await Country.GetActiveOrderedCountries()")
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Channelports)")
                .DisplayExpression("item.Code +  \" - \" + item.ToString()")
                .ChangeEventHandler("info.IsUKAddress = info.Country?.Code == \"GB\";");

            Field(x => x.AuthorisedCFSPCPCNumber)
                .Label("CFSP CPC Number")
                .Mandatory()
                .AsDropDown()
                .ReloadOnChange()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports)");

            Field(x => x.AuthorisedWarehouseNumber)
                .Label("Warehouse Number")
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.AuthorisedCFSPCPCNumber == CFSPCPCNumber._0612071");

            CustomField()
               .ControlMarkup("<h3>Local Officers Details</h3>")
               .NoLabel()
               .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.AuthorisedCFSPCPCNumber == CFSPCPCNumber._0612071");

            Field(x => x.LocalOfficerSupervisingOffice)
                .Label("Supervising Office")
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.AuthorisedCFSPCPCNumber == CFSPCPCNumber._0612071");

            Field(x => x.LocalOfficerStreet)
                .Label("Street")
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.AuthorisedCFSPCPCNumber == CFSPCPCNumber._0612071");

            Field(x => x.LocalOfficerCity)
                .Label("City")
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.AuthorisedCFSPCPCNumber == CFSPCPCNumber._0612071");

            Field(x => x.LocalOfficerPostcode)
                .Label("Postcode")
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.AuthorisedCFSPCPCNumber == CFSPCPCNumber._0612071");

            Field(x => x.LocalOfficerCountryCode)
                .Label("Country Code")
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.AuthorisedCFSPCPCNumber == CFSPCPCNumber._0612071");

            Field(x => x.SFDOnly).Control(ControlType.HorizontalRadioButtons)
                .Mandatory()
                .VisibleIf("info.CFSPType.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.EORINumber.HasValue()");

            Field(x => x.DefaultDeclarant).Control(ControlType.AutoComplete).SourceCriteria("!item.IsDeactivated&&!item.IsCreatedFromAPI");

            Field(x => x.PaymentType)
                .DisplayExpression(@"item.Code + "" - "" + item.Description")
                .SourceCriteria("!item.IsDeactivated ")
                .AsDropDown()
                .ReloadOnChange();

            Field(x => x.DefermentNumber)
                .VisibleIf("info.PaymentType != null")
                .Mandatory();

            Field(x => x.VATByDAN)
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons)
                .VisibleIf("info.PaymentType != null");

            Field(x => x.RepresentationType)
                .Control(ControlType.HorizontalRadioButtons);

            Field(x => x.GuarantorType)
                .AsRadioButtons(Arrange.Horizontal)
                .ReloadOnChange()
                .Label("Guarantor Type").DisplayExpression("item.DisplayName").Mandatory();

            Field(x => x.GuarantorName)
                 .VisibleIf("info.GuarantorType == GuarantorType.DifferentCompanyGuarantee")
               .AsAutoComplete()
               .SourceCriteria("item.GuarantorType == GuarantorType.Own && !item.IsDeactivated && !item.IsCreatedFromAPI")
               .Mandatory()
               .Label("Guarantor Name");

            Field(x => x.GuaranteeNumber)
                .VisibleIf("info.GuarantorType == GuarantorType.Own")
                .Mandatory()
                .Label("Transit Guarantee");

            Field(x => x.GuaranteeType)
                .Mandatory()
                .VisibleIf("info.GuarantorType == GuarantorType.Own");

            Field(x => x.TIN);
            Field(x => x.PIN);
            Field(x => x.AuthorisedLocationsLinks).AsCollapsibleCheckBoxList().SourceCriteria("!item.IsDeactivated");
            //Field(x => x.AuthorisedLocationsLinks).AsCollapsibleCheckBoxList().SourceCriteria("!item.IsDeactivated");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                // x.If("info.Item.IsNew && info.PaymentType == null").MessageBox("The Deferment Number field is required.").AndExit();
                //   x.If("info.Type != CompanyType.Other && info.TransactionTypes.None()").MessageBox("The Transaction type(s) field is required").AndExit();
              //  x.If("info.AuthorisedLocationsLinks.Count() > 10").MessageBox("You can select upto a maximum of 10 authorised locations").AndExit();
                x.If("!info.DefermentNumber_Visible")
                   .CSharp("info.DefermentNumber = null;info.Item.DefermentNumber = null;");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();
            });

            ViewModelProperty<bool>("IsUKAddress");

            OnBound("Set IsUKAddress")
                .Code(@"
                        info.IsUKAddress = info.Item.Country?.Code == ""GB"";
                    ");

            OnBound("set deferement").Code("info.DefermentNumber = info.DefermentNumber ?? info.Item.DefermentNumber;");

            LoadJavascriptModule("scripts/components/company-form.js");
            OnBound("get value for authorisation number").Code(@"if (info.CFSPType != null && info.CFSPType == CFSPType.Channelports)
               {info.AuthorisationNumber = AppSetting.CFSPChannelportsAuthorisationNumber;
               info.Item.AuthorisationNumber = AppSetting.CFSPChannelportsAuthorisationNumber;}");

            OnBound("get value for sfd ").Code(@"if (info.CFSPType == CFSPType.None)
               {info.SFDOnly = false;
               info.Item.SFDOnly = false;}");
        }
    }
}
