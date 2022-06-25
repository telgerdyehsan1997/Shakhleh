using Domain;
using MSharp;

namespace Modules
{
    class ConsignmentForm : FormModule<Domain.Consignment>
    {
        public ConsignmentForm()
        {
            HeaderText("Consignment Details");
            SecurityChecks("info.Item.IsNew || info.Item.IsEditable(GetUser())");

            Field(x => x.ConsignmentNumber)
                .Readonly();

            Field(x => x.UKTrader)
                .DisplayExpression("item.GetUkDisplayText()")
                .AsAutoComplete()
                .AfterControlContainer("[#BUTTONS(AddCompany)#]")
                .ReloadOnChange()
                .DataSource("await info.SetShipment.Company.GetTraderAssociatedCompanies(info.UKTrader_Text)")
                .ChangeEventHandler(@"await ChangeUKTrader(info);");

            Field(x => x.HasFullCustomDetails)
                .Label("Do you have full Custom Declaration details?")
                .Mandatory()
                .Control(ControlType.HorizontalRadioButtons)
                .ReloadOnChange()
                .VisibleIf("info.IsHasFullCustomDetails");


            Field(x => x.Partner)
                .DisplayExpression("item.GetPatnerText()")
                .AsAutoComplete()
                .AfterControlContainer("[#BUTTONS(AddPartnerCompany)#]")
                .Label("Partner name")
                .DataSource("await info.SetShipment.Company.GetPartnerAssociatedCompanies(info.Partner_Text)")
                .RequiredValidationMessage("The Partner field is required.");

            Field(x => x.Declarant)
                .AsAutoComplete()
                .AfterControlContainer("[#BUTTONS(AddDeclarantCompany)#]")
                .DataSource("await info.SetShipment.Company.GetDeclarentAssociatedCompanies(info.Declarant_Text)")
                .ReloadOnChange()
                .ChangeEventHandler(@"await ChangeDeclarant(info);")
                .RequiredValidationMessage("The Declarant field is required.");

            Field(x => x.UseEIDR)
               .Control(ControlType.HorizontalRadioButtons)
               .VisibleIf(@"info.SetShipment.IsInUK && info.SetShipment.Company.UsingEIDR == true && info.SetShipment.Company.CFSPTypeId.IsAnyOf(CFSPType.Own) == true")
               .Mandatory()
               .ReloadOnChange()
               .Label("Do you wish to use EIDR");


            Field(x => x.SequenceNumber)
                .VisibleIf(@"info.SetShipment.IsInUK && info.SetShipment.Company.UsingEIDR == true && info.SetShipment.Company.CFSPTypeId.IsAnyOf(CFSPType.Own) == true")
                .ReloadOnChange()
                .ChangeEventHandler(@"if(info.SequenceNumber.HasValue() && info.UseEIDR)
                { 
                    info.SequenceNumber = System.Text.RegularExpressions.Regex.Replace(info.SequenceNumber, @""\s + "", """");
                    info.Item.UCR = info.Item.CreateDUCR(info.SetShipment.Company.AuthorisationNumber, info.SequenceNumber);
                }")
                .Mandatory();

            Field(x => x.IntoUKType)
               .VisibleIf("await info.Item.IsGVMSAndInventoryPort(info.Shipment.Route?.UKPort) && info.SetShipment.IsInUK")
               .Label("Into UK Type")
               .Mandatory()
               .Control(ControlType.HorizontalRadioButtons)
               .ReloadOnChange()
               .DataSource("await Database.Of<PortType>().GetList()");

            Field(x => x.CFSPShipmentNumber)
              //  .ExtraControlAttributes("readonly")
                .VisibleIf(@"info.IsCFSPShipmentNumberVisible")
                .Mandatory();

            AutoSet(x => x.Guarantor).OnlyIf("!info.SetShipment.IsInUK")
                    .Value("await GetGuarantor(info)");

            CustomField("Guarantor read only")
              .Label("Guarantor")
              .VisibleIf("!info.SetShipment.IsInUK")
              .ControlMarkup("@info.Guarantor?.Name");

            Field(x => x.UCN)
                .VisibleIf("info.IntoUKType == PortType.Inventory && info.SetShipment.IsInUK && Domain.Settings.Current.ActivateUCN == true");
            
            Field(x => x.UseSpecialCPC)
                .ReloadOnChange()
                .Control(ControlType.HorizontalRadioButtons)
                .VisibleIf("(await GetSpecialCPCs(info)).Any() && info.VisibleCFSP && info.UKTrader != null && info.UKTrader.UsingEIDR == false")
                .CustomDataSave(@"if(!info.UseSpecialCPC){item.SpecialCPC = null;}");
            
            Field(x => x.SpecialCPC)
                 .VisibleIf("info.UseSpecialCPC && (await GetSpecialCPCs(info)).Any() && info.VisibleCFSP")
                 .Mandatory()
                 .DataSource(@"await GetSpecialCPCs(info)")
                 .AsAutoComplete();

            Field(x => x.TotalPackages);
               
            Field(x => x.TotalGrossWeight)
                .Mandatory()
                .VisibleIf("info.VisibleCFSP")
                .AfterControlAddon("kg");

            Field(x => x.TotalNetWeight)
                .Mandatory()
                .VisibleIf("info.VisibleCFSP")
                .AfterControlAddon("kg");

            Field(x => x.InvoiceNumber)
                .AfterControlContainer("[#BUTTONS(AddSecondInvoiceNumber)#]");

            Button("Add Second Invoice Number")
              .NoText()   
              .Icon(FA.Plus)
              .CssClass("float-left")
              .CausesValidation(false)
              .VisibleIf("!info.SecondInvoiceNumber_Visible")
              .OnClick(x =>
              {
                  x.CSharp("info.HasSecondBorder = true; info.SecondInvoiceNumber_Visible = true; return View(info);");
              });

            Button("Removed Second Invoice Number")
            .NoText()
            .Icon(FA.Remove)
            .CssClass("float-left btn-danger")
            .CausesValidation(false)
            .VisibleIf("info.SecondInvoiceNumber_Visible")
            .OnClick(x =>
            {
                x.CSharp("info.HasSecondBorder = false; info.SecondInvoiceNumber_Visible = false; return View(info);");
            });

            Field(x => x.SecondInvoiceNumber)
               .VisibleIf("info.HasSecondBorder || info.Item?.SecondInvoiceNumber != null")
               .AfterControlContainer("[#BUTTONS(AddThirdInvoiceNumber)#] [#BUTTONS(RemovedSecondInvoiceNumber)#]");

            Button("Add Third Invoice Number")
               .NoText()
               .VisibleIf("!info.ThirdInvoiceNumber_Visible")
               .Icon(FA.Plus)
               .CssClass("float-left")
               .CausesValidation(false)
               .OnClick(x =>
               {
                   x.CSharp("info.HasThirdBorder = true; info.ThirdInvoiceNumber_Visible = true; return View(info);");
               });

            Button("Removed Third Invoice Number")
              .NoText()
              .VisibleIf("info.ThirdInvoiceNumber_Visible")
              .Icon(FA.Remove)
              .CssClass("float-left  btn-danger")
              .CausesValidation(false)
              .OnClick(x =>
              {
                  x.CSharp("info.HasThirdBorder = false; info.ThirdInvoiceNumber_Visible = false; return View(info);");
              });


            Field(x => x.ThirdInvoiceNumber)
                .VisibleIf("info.HasThirdBorder || info.Item?.ThirdInvoiceNumber != null")
                .Label("Third invoice number")
                .AfterControlContainer("[#BUTTONS(AddFourthInvoiceNumber)#] [#BUTTONS(RemovedThirdInvoiceNumber)#]");


            Button("Add Fourth Invoice Number")
                .NoText()
                .VisibleIf("!info.FourthInvoiceNumber_Visible")
                .Icon(FA.Plus)
                .CssClass("float-left")
                .CausesValidation(false)
                .OnClick(x =>
                {
                    x.CSharp("info.HasFourthBorder = true; info.FourthInvoiceNumber_Visible = true; return View(info);");
                });

            Button("Removed Fourth Invoice Number")
               .NoText()
               .VisibleIf("info.FourthInvoiceNumber_Visible")
               .Icon(FA.Remove)
               .CssClass("float-left  btn-danger")
               .CausesValidation(false)
               .OnClick(x =>
               {
                   x.CSharp("info.HasFourthBorder = false; info.FourthInvoiceNumber_Visible = false; return View(info);");
               });

            Field(x => x.FourthInvoiceNumber)
                .VisibleIf("info.HasFourthBorder || info.Item?.FourthInvoiceNumber != null")
                .Label("Fourth invoice number")
                .AfterControlContainer("[#BUTTONS(RemovedFourthInvoiceNumber)#]");



            Field(x => x.InvoiceCurrency)
                .Mandatory()
                .AsAutoComplete(true)
                .VisibleIf("info.VisibleCFSP")
                .DataSource("await Helper.GetCurrencyList(info.SetShipment)");

            Field(x => x.TotalValue)
                .Mandatory()
                .VisibleIf("info.VisibleCFSP");

            Field(x => x.TermsOfSale)
                .Mandatory()
                .DisplayExpression("item.Name + \" - \" + item.Description")
                .ReloadOnChange()
                .VisibleIf("info.VisibleCFSP")
                .DataSource("await GetTermOfSales()");

            Field(x => x.FreightCurrency)
                .AsAutoComplete()
                .Mandatory()
                .VisibleIf("info.IsImporterPayingTheFreight && info.VisibleCFSP")
                .DataSource("await Database.GetList<Currency>()");

            Field(x => x.FreightAmount)
                .Mandatory()
                .VisibleIf("info.IsImporterPayingTheFreight && info.VisibleCFSP");

            Field(x => x.GuaranteeLength)
                .Mandatory()
                .VisibleIf(@"info.SetShipment.AuthorisedLocation != null && info.SetShipment.UseAuthorisedLocation == true && 
                             info.GuaranteeLength_Options.Count() > 0 && 
                             await info.SetShipment.AuthorisedLocation?.GuaranteeLengths?.Where(x => !x.IsDeactivated).Count()>1 && info.VisibleCFSP")
                .DisplayExpression("item.Length")
                .DataSource("info.SetShipment.AuthorisedLocation != null ? await info.SetShipment.AuthorisedLocation?.GuaranteeLengths?.Where(x => !x.IsDeactivated).GetList() : new List<GuaranteeLength>()");

            Field(x => x.DDPOptions)
                .Mandatory()
                .VisibleIf("info.TermsOfSale?.IsDDP == true && info.SetShipment.IsInUK && info.VisibleCFSP")
                .AsRadioButtons(Arrange.Horizontal);

            AutoSet(x => x.VAT).Value("info.TermsOfSale?.ValueForVAT");

            Field(x => x.IsImporterPayingInsuranceCharges)
                .VisibleIf("info.SetShipment.IsInUK && (info.TermsOfSale?.FreightCharge == true) && info.VisibleCFSP")
                .ReloadOnChange()
                .Control(ControlType.HorizontalRadioButtons)
                .CustomDataSave(@"if(!info.IsImporterPayingInsuranceCharges)
                    {item.InsuranceCurrency = null; item.InsuranceAmount = null;}")
                .Label(@"@($""Is {info.ImporterOrExporter} paying insurance charges"")");

            Field(x => x.InsuranceCurrency)
                .AsAutoComplete()
                .Mandatory()
                .VisibleIf("info.IsImporterPayingInsuranceCharges && info.VisibleCFSP")
                .DataSource("await Database.GetList<Currency>()");

            Field(x => x.InsuranceAmount)
                .Mandatory()
                .VisibleIf("info.IsImporterPayingInsuranceCharges && info.VisibleCFSP")
                .ItemCssClass("readonly-field");

            CustomField("UCR")
                .PropertyName("UCR")
                .Label("Declaration Unique Consignment Reference (DUCR)")
                .Readonly();

            Field(x => x.Only1Commodity)
                .Control(ControlType.HorizontalRadioButtons)
                .Label("Only 1 Commodity")
                .VisibleIf("info.VisibleCFSP");


            AutoSet(x => x.Shipment).Value("info.SetShipment");
            AutoSet(x => x.ConsignmentNumber).Value("info.Item.ConsignmentNumber ?? await info.Item.GenerateConsignmentNumber(info.SetShipment)");
            AutoSet(x => x.IdNumber).Value("info.Item.IdNumber ?? await info.Item.GenerateIdNumber(info.SetShipment)");
            AutoSet(x => x.IsImporterPayingTheFreight).Value("info.SetShipment.IsInUK && (info.TermsOfSale?.FreightCharge == true)");


            Button("Cancel").OnClick(x => x.Go<Share.Consignments.ConsignmentsPage>().Pass("shipment"));

            Button("Save and Add Commodities").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {

                x.RunInTransaction();
                x.CSharp("var oldInfo = info.Item;");
                x.If("(await info.SetShipment.Consignments.Count()) == 99 && info.Item.IsNew")
                    .MessageBox("You cannot have more than 99 consignments per shipment").AndExit();
                x.If("info.FreightAmount_Visible && info.FreightAmount < 0.01m")
                .MessageBox("The value of Freight amount must be 0.01 or more.").AndExit();
                x.SaveInDatabase();
                x.CSharp(@"if(info.IntoUKType == PortType.Inventory && info.SetShipment.IsInUK && Domain.Settings.Current.ActivateUCN == true && info.UCN.IsEmpty()) 
                          await EmailTemplate.SendInventoryUsedWithNoUCN(info.Shipment.MyReferenceForCPInvoice, info.ConsignmentNumber, info.Shipment.Route.UKPort?.DTIBadge);");
                x.CSharp("await Shipment.UpdateProgress(info.Item.Shipment);").ValidationError();
                x.CSharp("await UpdateUkTraderAndPartnersToCompany(info, oldInfo);").ValidationError();
                x.GentleMessage("Saved successfully.");
                x.CSharp(@"ClearSession();");
                x.If("info.Only1Commodity && await info.Item.CanAddCommodity(GetUser())").Go<Share.Commodities.CommodiyEnterPage>().Send("consignment", "info.Item.ID");
                x.Else().Go<Share.Commodities.CommoditiesPage>()
                .Send("consignment", "info.Item.ID")
                .Send("ReturnUrl", "GetReturnURL(info)");
            });

            OnBeforeSave("UCR Update")
                .Code(@"if(info.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Channelports) && info.UKTrader.UsingEIDR == true)
                    {   if (info.Item.IsNew)
                            await Settings.SetCFSPShipmentNumber();
                    }");


            Button("Add company").CausesValidation(false).Icon(FA.Plus).CssClass("float-left").NoText().OnClick(x =>
            {
                x.PopUp<Share.Utils.AddCompanyPage>().Send("isPartner", "false").Send("isTrader", "true").Send("fieldname", "\"uktrader\"");
            });

            Button("Add partner company").CausesValidation(false).Icon(FA.Plus).CssClass("float-left").NoText().OnClick(x =>
            {
                x.PopUp<Share.Utils.AddCompanyPage>().Send("isPartner", "true").Send("fieldname", "\"partner\"");
            });


            Button("Add declarant company").CausesValidation(false).Icon(FA.Plus).CssClass("float-left").NoText().OnClick(x =>
            {
                x.PopUp<Share.Utils.AddCompanyPage>().Send("isPartner", "false").Send("fieldname", "\"declarant\"");
            });

            ViewModelProperty<bool>("HasLoaded").RetainInPost();

            OnBound_GET("Check HasLoaded").Code(@"if(!info.HasLoaded){

                info.HasLoaded = true;
                info.UseEIDR = true;
            }");

            OnBound("if only code supply by user").Code(@"if(info.InvoiceCurrency_Text.HasValue() && info.InvoiceCurrency == null)
                info.InvoiceCurrency = await Database.Of<Currency>().Where(x => x.Name == info.InvoiceCurrency_Text).FirstOrDefault() ?? null;");

            OnBound("set into uk type").Code(@"if(info.IntoUKType == null && await info.Item.IsGVMSAndInventoryPort(info.SetShipment.Route?.UKPort) == false && info.SetShipment.IsInUK)
                              {
                                var type=await info.Item.ConsigmentsPort(info.SetShipment.Route?.UKPort);
                                info.Item.IntoUKType = type;
                                info.IntoUKType = type;
                             }

            ");

            ViewModelProperty<Shipment>("SetShipment").FromRequestParam("shipment");

            ViewModelProperty<string>("ImporterOrExporter");

            OnBound_GET("OnBoundGet").Code(@"await OnBoundGet(info);");

            OnPostBound("override").Code(@"if(Request.IsGet())
            {
                info.UKTrader_Text = info.UKTrader?.GetUkDisplayText();
                info.Partner_Text = info.Partner?.GetPatnerText();
            }");

            OnPostBound("OnPostBound").Code(@"await OnPostBound(info);");
            LoadJavascriptModule("scripts/components/consignment-form.js");

            ViewModelProperty<bool>("HasSecondBorder").RetainInPost().NotReadOnly();
            ViewModelProperty<bool>("HasThirdBorder").RetainInPost().NotReadOnly();
            ViewModelProperty<bool>("HasFourthBorder").RetainInPost().NotReadOnly();


            ViewModelProperty<bool>("IsCFSPOptionVisiblity").OnBound("info.IsCFSPOptionVisiblity = info.SetShipment.IsInUK &&  info.UKTrader != null && info.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Own, CFSPType.Channelports) && info.UKTrader.SFDOnly == true;");
            ViewModelProperty<bool>("IsHasFullCustomDetails").OnBound(@"info.IsHasFullCustomDetails = info.IsCFSPOptionVisiblity && info.UKTrader != null &&  info.UKTrader.UsingEIDR == false;");
            ViewModelProperty<bool>("IsCFSPShipmentNumberVisible").OnBound(@"info.IsCFSPShipmentNumberVisible = info.SetShipment.IsInUK && info.UKTrader != null && info.UKTrader.CFSPTypeId.IsAnyOf(CFSPType.Own) && info.UKTrader.UsingEIDR == false;");
            ViewModelProperty<bool>("IsSDFOnly").OnBound("info.IsSDFOnly = info.SetShipment.IsInUK &&  info.UKTrader != null && info.UKTrader.SFDOnly == true;");
            ViewModelProperty<bool>("VisibleCFSP").OnBound(@"if(info.IsCFSPOptionVisiblity && info.UKTrader.UsingEIDR == true)
                                                                 info.VisibleCFSP = true;
                                                            else
                                                               info.VisibleCFSP = info.SetShipment.IsInUK && info.IsCFSPOptionVisiblity ? info.HasFullCustomDetails == true : info.IsSDFOnly ? false : true;");
        }
    }
}