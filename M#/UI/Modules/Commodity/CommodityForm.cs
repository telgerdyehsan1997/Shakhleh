using MSharp;
using System.Collections;
using System.Collections.Generic;

namespace Modules
{
    class CommodityDetails : FormModule<Domain.Commodity>
    {
        public CommodityDetails()
        {
            SecurityChecks("info.Consignment.IsEditable(GetUser())");
            HeaderText("Commodity Details");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            ViewModelProperty<bool>("HasFullCustomDetails").OnBound("info.HasFullCustomDetails = info.Consignment.IsHaveAllDeclaraionDetails();");
            AutoSet(x => x.Consignment);

            Field(x => x.Product).Label("Product code")
                .DataSource("await info.Consignment.GetUkTraderOrCompanyProducts(orderResult: true)")
                .Control(ControlType.AutoComplete)
                .ReloadOnChange()
                .ChangeEventHandler("await CommodityDocuments(info);")
                .VisibleIf("info.HasFullCustomDetails")
                .AfterControlContainer("[#BUTTONS(AddProduct)#]");

            Field(x => x.VAT)
                .DataSource("(info.Product?.CommodityCode == null ? Enumerable.Empty<VATType>() : await info.Product?.CommodityCode?.MultipleVAT).ToList()")
                .AsRadioButtons(Arrange.Horizontal)
                .Mandatory()
                .VisibleIf("((info.Product?.CommodityCode == null ? Enumerable.Empty<VATType>() : await info.Product?.CommodityCode?.MultipleVAT).ToList()).HasMany() && info.Consignment.Shipment.IsInUK && info.HasFullCustomDetails")
                .AutoSet(x => { x.Value(@"info.Product != null ? info.Product?.VAT ?? await info.Product?.CommodityCode?.GetSOrFirst() : await VATType.FindByName(""S"")").OnlyIf("info.VAT == null"); })
                .CustomDataSave(@"item.VAT = info.VAT ?? (info.Product?.VAT ?? await info.Product.CommodityCode.GetSOrFirst());");


            CustomField("CommodityCode").Label("Commodity Code").ItemCssClass("readonly-field")
            .ControlMarkup(@"@info.Product?.CommodityCode?.ExportCode").VisibleIf(@"info.Product != null");

            Field(x => x.GrossWeight)
                .Mandatory()
                .VisibleIf("info.HasFullCustomDetails");

            Field(x => x.NetWeight)
                .VisibleIf("info.HasFullCustomDetails")
                .Mandatory();

            CustomField("Second quantity").Label("Second quantity").ItemCssClass("readonly-field")
            .ControlMarkup(@"@info.Product?.CommodityCode?.SecondQuantity?.QuantityCode")
            .AfterControl(@"@info.Product?.CommodityCode?.SecondQuantity?.Description")
            .VisibleIf("info.Product?.CommodityCode?.SecondQuantity?.QuantityCode.HasValue() ?? false");

            Field(x => x.SecondQuantity)
            .VisibleIf("(info.Product?.CommodityCode?.SecondQuantity?.QuantityCode.HasValue() ?? false) && info.HasFullCustomDetails").Mandatory();

            CustomField("Third quantity").Label("Third quantity").ItemCssClass("readonly-field")
                .ControlMarkup(@"@info.Product?.CommodityCode?.ThirdQuantity?.QuantityCode")
                .AfterControl(@"  @info.Product?.CommodityCode?.ThirdQuantity?.Description")
                .VisibleIf("(info.Product?.CommodityCode?.ThirdQuantity?.QuantityCode.HasValue() ?? false) && info.HasFullCustomDetails");

            Field(x => x.ThirdQuantity).VisibleIf("(info.Product?.CommodityCode?.ThirdQuantity?.QuantityCode.HasValue() ?? false) && info.HasFullCustomDetails").Mandatory();

            CustomField("Currency")
                .Label("Currency")
                .VisibleIf("info.HasFullCustomDetails")
                .ControlMarkup("@(info.Consignment?.InvoiceCurrency?.ToStringOrEmpty())");

            Field(x => x.Value)
                .VisibleIf("info.HasFullCustomDetails");

            Field(x => x.NumberOfPackages)
                .VisibleIf("info.HasFullCustomDetails")
                .Label("Number of packages for this commodity code (if known)");

            Field(x => x.CountryOfDestination)
               .Control(ControlType.AutoComplete)
               .Label("@(info.Consignment.Shipment.IsInUK ? \"Country of origin\":\"Country of destination\")")
               .DataSource("await Country.GetActiveOrderedCountries(includeUK: false)")
               .DisplayExpression("item.Code +  \" - \" + item.ToString()")
               .ReloadOnChange()
               .ChangeEventHandler(@"if(info.CountryOfDestination?.PreferenceAvailable == false){
                            info.HasPreference = false;
                            info.PreferenceType = null; 
                            info.PreferenceCertificateNumber = string.Empty;}")
               .RequiredValidationMessage("The Country field is required.");

            Field(x => x.DescriptionOfGoods)
                .VisibleIf("info.Consignment.IsHaveCFSP()");

            Field(x => x.HasPreference)
                .Label("Preference")
                .Control(ControlType.HorizontalRadioButtons)
                .Mandatory()
                .RequiredValidationMessage("The Preference field is required.")
                .VisibleIf("info.CountryOfDestination != null && info.CountryOfDestination.PreferenceAvailable == true && info.HasFullCustomDetails")
                .CustomDataSave(@"if(info.HasPreference == null || info.HasPreference == false){
                            item.HasPreference = false;
                            item.PreferenceType = null; 
                            item.PreferenceCertificateNumber = string.Empty;}")
                .ChangeEventHandler(@"if (info.Consignment.Shipment.IsInUK && info.CountryOfDestination.EU27 == true && info.HasPreference == false)
                    {
                        Notify(""You have declared the origin as being from the EU - originating in the EU are entitled to Preference claim. Duty will be payable if you answer NO to preference."");
                    }")
            .ReloadOnChange();

            Field(x => x.PreferenceType)
                .Mandatory()
                .VisibleIf(@"await IsPreferenceTypeVisible(info) && info.HasFullCustomDetails")
                .AsRadioButtons(Arrange.Horizontal)
                .ReloadOnChange()
                .DataSource(@"await GetPreferenceTypes()")
                .ChangeEventHandler("if(info.PreferenceType != PreferenceType.PreferenceCertificateNumber) info.PreferenceCertificateNumber = string.Empty;")
                .AfterControl("[#BUTTONS(Invoice)#]");

            Field(x => x.PreferenceCertificateNumber).Mandatory().ReloadOnChange()
                .VisibleIf(@"await IsPreferenceCertificateNumberVisible(info) && info.HasFullCustomDetails");

            Field(x => x.GoodsLicencable)
                .Control(ControlType.HorizontalRadioButtons)
                .Label("Are the goods licencable?").ReloadOnChange()
                .VisibleIf("info.HasFullCustomDetails")
                .CustomDataSave(@"if(!info.GoodsLicencable){item.LicenceType = null; item.LicenceNumber = """";}");

            Field(x => x.LicenceType).Label("Licence name").Control(ControlType.AutoComplete).Mandatory().VisibleIf("info.GoodsLicencable && info.HasFullCustomDetails")
                .SourceCriteria("!item.IsDeactivated && item.Type == info.Consignment.Shipment.Type")
                .ReloadOnChange();

            Field(x => x.LicenceNumber)
                .Mandatory()
                .VisibleIf("info.GoodsLicencable && info.HasFullCustomDetails");

            CustomField("LicenceStatusCode")
                .Label("Licence Status Code")
                .ItemCssClass("readonly-field")
                .ControlMarkup("@info.LicenceType?.LicenceStatusCode?.StatusCode")
                .Mandatory()
                .VisibleIf("info.GoodsLicencable && info.HasFullCustomDetails");

            AutoSet(x => x.LicenceStatusCode).Value("@info.LicenceType?.LicenceStatusCode");

            Field(x => x.Quantity).Mandatory().VisibleIf("info.GoodsLicencable && info.LicenceType?.Quantity == true && info.HasFullCustomDetails");
            Field(x => x.RPTIDCode).Mandatory().VisibleIf("info.GoodsLicencable && info.LicenceType?.RPTID == true && info.HasFullCustomDetails");

            Field(x => x.NeedPHYTODocumentNumber)
               .Control(ControlType.HorizontalRadioButtons)
               .Label("Do you Need a PHYTO?")
               .ChangeEventHandler("await CommodityDocuments(info);")
               .Mandatory()
               .VisibleIf("info.Product?.CommodityCode?.N851_PHC == true && info.Consignment.Shipment.IsInUK")
               .ReloadOnChange();

            Field(x => x.PHYTODocumentNumber)
                .Mandatory()
                .CustomDataSave("item.PHYTODocumentNumber = info.PHYTODocumentNumber;")
                .VisibleIf("info.Product?.CommodityCode?.N851_PHC == true && info.NeedPHYTODocumentNumber == true");

            Field(x => x.NeedIPAFFDocumentNumber)
               .Control(ControlType.HorizontalRadioButtons)
               .Label("Do you Need an IPAFF?")
               .ChangeEventHandler("await CommodityDocuments(info);")
               .Mandatory()
               .VisibleIf("(info.Product?.CommodityCode?.N852_CED == true || info.Product?.CommodityCode?.N853_CVD == true) && info.Consignment.Shipment.IsInUK")
               .ReloadOnChange();

            Field(x => x.IPAFFDocumentNumber)
                .Mandatory()
                .CustomDataSave("item.IPAFFDocumentNumber = info.IPAFFDocumentNumber;")
                .VisibleIf("(info.Product?.CommodityCode?.N852_CED == true || info.Product?.CommodityCode?.N853_CVD == true) && info.NeedIPAFFDocumentNumber == true");

            Field(x => x.IsApplyForAll)
              .Control(ControlType.HorizontalRadioButtons)
              .Label("Do you wish to use same number on all applicable items")
              .Mandatory()
              .VisibleIf("(info.NeedPHYTODocumentNumber == true ||  info.NeedIPAFFDocumentNumber == true ) && info.Consignment.Shipment.IsInUK && await info.Item.CommoditiesWithApply(await info.Consignment.Commodities.GetList(), info.Item.ProductId.HasValue ? info.Item.ProductId : info.Product.ID, info.Item.IsNew)");

            Field(x => x.AreTheGoodHazardous)
                .Control(ControlType.HorizontalRadioButtons)
                .Mandatory()
                .VisibleIf(" info.Consignment.Shipment.SafetyAndSecurity == true && info.HasFullCustomDetails")
                .ReloadOnChange();

            Field(x => x.HaveHealthCertificateNumber)
               .ChangeEventHandler(@"if(info.HaveHealthCertificateNumber == true)
                                      { 
                                             while (info.HealthCertificate.Count() < 1)
                                            {
                                                info.HealthCertificate.Add(new vm.CommodityDetails.HealthCertificateSubForm());
                                                await Task.WhenAll(info.HealthCertificate.Select(OnBound));
                                            }
                                      }
               ")
               .Mandatory()
               .Label("Have Health Certificate Number")
               .Control(ControlType.HorizontalRadioButtons)
               .VisibleIf("info.Consignment.Shipment.IsOutUK")
               .ReloadOnChange();

            MasterDetail(x => x.HealthCertificate, x =>
            {
               x.Field(x => x.Number)
                .Mandatory()
                .Label("Health Certificate Number");

                x.Field(x => x.HealthCertificate)
                .SourceCriteria("!item.IsDeactivated")
                 .Mandatory()
                 .Control(ControlType.VerticalRadioButtons)
                 .DisplayExpression("item.Name")
                 .Label("Health Certificate Code");

                x.Button("AddAnother")
                .NoText()
                .Icon(FA.Plus)
                .CssClass("float-right")
                .CausesValidation(false)
                .OnClick(x => x.AddMasterDetailRow());

               x.Orientation(Arrange.Vertical);

            }).MinCardinality(1)
              .MaxCardinality(8)
              .NoLabel()
              .VisibleIf("info.HaveHealthCertificateNumber == true");


            Field(x => x.UNCode).VisibleIf("info.AreTheGoodHazardous && info.HasFullCustomDetails");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());
            Button("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.CSharp(@"try { await info.Consignment.ResetStatus(); } catch(Exception ex) { return Notify(ex.Message, ""error""); }");
                x.CSharp("await info.Item.UpdateComodityDutyPay(info.Item);");
                x.CSharp("info.Consignment.Shipment.AreWeightsMismatched();");
                x.GentleMessage("Saved successfully.");
                x.If("info.Consignment.Only1Commodity").Go<Share.Commodities.CommoditiesPage>().Send("consignment", "info.Consignment.ID");
                x.Else().ReturnToPreviousPage();
            });

            Button("AddProduct").CausesValidation(false).NoText().Icon(FA.Plus).CssClass("float-left")
            .OnClick(x =>
            {
                x.PopUp<Share.Utils.AddProductPage>().Pass("consignment");
            });

            Button("Example of REX Preferential Statement")
            .Style(ButtonStyle.Link)
             .Name("Invoice")
             .OnClick(x => x.PopUp<Share.Commodities.InvoiceDeclarationRulesPage>());

            OnBound_GET("set consignment for readonly")
                .Code("info.Item.Consignment = info.Item.IsNew ? info.Consignment : info.Item.Consignment;");
            OnBound_GET("set consignment for readonly")
                .Criteria("info.Item.IsNew && info.Consignment.Only1Commodity")
                .Code(@"info.NetWeight = info.Consignment.TotalNetWeight;
                        info.GrossWeight = info.Consignment.TotalGrossWeight;
                        info.Value = info.Consignment.TotalValue;
                        info.NumberOfPackages = info.Consignment.TotalPackages;");



            OnControllerClassCode("update fields based on apply").Code(@" private static async Task CommodityDocuments(vm.CommodityDetails info)
                    {
                        if (info.Consignment.Shipment.IsInUK && (info.Product?.CommodityCode?.N851_PHC == true || info.Product?.CommodityCode?.N852_CED == true || info.Product?.CommodityCode?.N853_CVD == true))
                        {
                            var commodityList =await info.Consignment.Commodities.Where(x => x.IsApplyForAll == true).GetList();
                            
                            if (commodityList.Any(x => x.IsApplyForAll == true))
                            {
                                if (info.Product?.CommodityCode?.N851_PHC == true)
                                {
                                    var commodity =  commodityList.Where(x => x.PHYTODocumentNumber.HasValue()).FirstOrDefault();
                                    info.NeedPHYTODocumentNumber = info.NeedPHYTODocumentNumber ?? commodity?.NeedPHYTODocumentNumber;
                                    
                                    if (info.NeedPHYTODocumentNumber == true)
                                    {
                                        info.PHYTODocumentNumber = info.Item.PHYTODocumentNumber.HasValue() ? info.Item.PHYTODocumentNumber : commodity?.PHYTODocumentNumber;
                                        info.PHYTODocumentNumber_Visible = true;
                                    }
                                }
                                
                                if (info.Product?.CommodityCode?.N852_CED == true || info.Product?.CommodityCode?.N853_CVD == true)
                                {
                                    var commodity =  commodityList.Where(x=>x.IPAFFDocumentNumber.HasValue()).FirstOrDefault();
                                    
                                    if (info.NeedIPAFFDocumentNumber == true)
                                    {
                                        info.IPAFFDocumentNumber = info.Item.IPAFFDocumentNumber.HasValue() ? info.Item.IPAFFDocumentNumber : commodity?.IPAFFDocumentNumber;
                                        info.IPAFFDocumentNumber_Visible = true;
                                    }
                                }
                            }
                        }
                    }");
        }
    }
}