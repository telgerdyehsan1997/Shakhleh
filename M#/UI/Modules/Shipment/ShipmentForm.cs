using System;
using Domain;
using MSharp;

namespace Modules
{
    class ShipmentForm : FormModule<Domain.Shipment>
    {
        public ShipmentForm()
        {
            this.AddDependency<ILookupService>();

            HeaderText("Shipment Details");
            SecurityChecks("info.IsAvalible(CompanyUser,ChannelPortsUser)");

            AutoSet(x => x.Company)
                .Value("CompanyUser.Company;")
                .OnlyIf("CompanyUser != null");

            Field(x => x.Company)
                .AsAutoComplete()
                .Label("Company name")
                .VisibleIf("CompanyUser == null && ChannelPortsUser != null")
                .RequiredValidationMessage("The Company name field is required")
                .ReloadOnChange()
                .DataSource("await LookupService.GetActiveCompanyList()")
                .ChangeEventHandler(@"await ReloadCompany(info);");

            Field(x => x.Type).VisibleIf("info.Item.IsNew")
                .CustomDataLoad(@"if (CompanyUser != null && await CompanyUser.Company.TransactionTypes.Count() == 1)
                                    info.Type = await CompanyUser.Company.TransactionTypes.GetList().Select(x => x.ShipmentType).FirstOrDefault();
                                  else
                                    info.Type = info.Item?.Type;")
                .DisplayExpression("item.DisplayName")
                .Mandatory().Control(ControlType.HorizontalRadioButtons).ReloadOnChange().ChangeEventHandler("return View(info);");

            CustomField().Label("Type").VisibleIf("!info.Item.IsNew").ControlMarkup("@Model.Item.Type.DisplayName");

            Field(x => x.IsNCTSShipmentOutConvertible)
                .Mandatory()
                .Label("NCTS")
                .ReloadOnChange()
                .AsRadioButtons(Arrange.Horizontal)
                .VisibleIf("info.CanCheckNCTS");

            CustomField().Label("NCTS")
                .VisibleIf("info.ShowNCTSLabel")
                .ControlMarkup(@"@(info.Company.NCTS == NCTSType.Always ? ""Yes"" : ""No"")");

            Field(x => x.Route).Mandatory()
               .AsAutoComplete()
               .SourceCriteria(@"!item.IsDeactivated")
               .DisplayExpression(@"info.Type.Name == ShipmentType.IntoUk.Name ? item.Non_UKPort.PortName + "" to "" + item.UKPort.PortName:item.UKPort.PortName + "" to "" + item.Non_UKPort.PortName")
               .RequiredValidationMessage("The Route field is required.")
               .ChangeEventHandler(@"info.Item.FirstBorderCrossing = info.Route?.Non_UKPort?.TransitOffice;
                                        return View(info);")
               .VisibleIf("info.Item.FullEdit");

            CustomField()
            .ControlMarkup("@info.Item.Route")
            .Label("Route")
            .VisibleIf("!info.Item.FullEdit");

            //Field(x => x.GVMS)
            // .VisibleIf("info.PortOfArrival?.IntoUKType == PortType.GVMS");

            //Field(x => x.IsGVMS)
            //.Mandatory()
            //.AsRadioButtons(Arrange.Horizontal)
            //.Label("GVMS")
            //.ReloadOnChange()
            //.VisibleIf("info.ShowGVMS()");

            Field(x => x.SafetyAndSecurity)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal)
                .ReloadOnChange()
                .VisibleIf("info.ShowSafetyAndSecurity()");

            CustomField().Label("Safety And Security")
                .VisibleIf("info.AlwaysShowSafetyAndSecurity()")
                .ControlMarkup(@"@((info.Type == ShipmentType.OutOfUk && info.Company.SafetyAndSecurityOutboundId.IsAnyOf(Domain.SafetyAndSecurity.Always)) || (info.Type == ShipmentType.IntoUk && info.Company.SafetyAndSecurityInboundId.IsAnyOf(Domain.SafetyAndSecurity.Always)) ? ""Yes"" : ""No"")");

            //Field(x => x.AreThereMRNs).VisibleIf("info.Item.GVMS");
            //Field(x => x.AreMRNsAvailableNow);
            //Field(x => x.MRN);


            CustomField().Label("Company type")
                .VisibleIf("CompanyUser == null && ChannelPortsUser != null && info.Company != null")
                .ControlMarkup("@info.Company?.Type");

            Field(x => x.PrimaryContact)
                .Control(ControlType.AutoComplete)
                .ChangeEventHandler(@"ClearSession(""primary_contactId_"" + User.GetId());")
                .DataSource("(info.Company != null ? await info.Company?.GetAvailableContacts() : new List<Person>())")
                .AfterControlContainer("[#BUTTONS(AddContact)#]");

            Field(x => x.NotifyAdditionalParty).Mandatory()
                .Label("Notify additional parties").ReloadOnChange().Control(ControlType.HorizontalRadioButtons);

            Field(x => x.Group).Mandatory()
                .VisibleIf("info.NotifyAdditionalParty == NotifyType.Group").DataSource("info.Company != null ? await info.Company?.ContactGroups?.GetList().Except(x => x.IsDeactivated) : new List<ContactGroup>()");

            Field(x => x.ContactNameLinks).Mandatory().Control(ControlType.CollapsibleCheckBoxList)
                .Label("Contact name")
                .VisibleIf("info.NotifyAdditionalParty == NotifyType.SpecificContacts")
                .DisplayExpression("item.Name")
                .DataSource("(info.Company != null ? await info.Company?.GetAvailableContacts() : new List<Person>())");

            Field(x => x.MyReferenceForCPInvoice)
               .Mandatory()
               .Label("Customer Reference");

            Field(x => x.VehicleNumber);
            Field(x => x.TrailerNumber);
            Field(x => x.ContainerNumber)
                .VisibleIf("info.SafetyAndSecurity == true");

            Field(x => x.Unaccompanied)
              .Mandatory()
              .Control(ControlType.HorizontalRadioButtons)
              .VisibleIf("info.SafetyAndSecurity == true && info.Type == ShipmentType.IntoUk");

            Field(x => x.Carrier)
              .AsAutoComplete()
              .AfterControlContainer("[#BUTTONS(AddCarrier)#]")
              .ReloadOnChange()
              .ChangeEventHandler(@"ClearSession(""carrier_contactId_"" + User.GetId());")
              .Mandatory()
              .DataSource("await Carrier.GetCarriers()")
              .VisibleIf("info.SafetyAndSecurity == true && info.Type == ShipmentType.IntoUk");

            CustomField("ExpectedDate")
                .AsDatePicker()
                .ControlMarkup(@"<input type=""text"" asp-for=""ExpectedDate"" asp-format=""{0:dd/MM/yyyy}"" class=""form-control"" data-control=""date-picker""/>")
                .VisibleIf(@"Model.IsChannelportUser&&Model.Type==ShipmentType.IntoUk")
                .Label(@"@(info.Type == ShipmentType.IntoUk ? ""Expected date of arrival"" : ""Expected date of departure"")");

            Field(x => x.ExpectedDate)
                .VisibleIf(@"!(info.IsChannelportUser&&info.Type==ShipmentType.IntoUk)")
                .ExtraControlAttributes(@"data-disable-past=""true"" data-min-past=""@((!info.Item.IsNew && Model.ExpectedDate < LocalTime.Now ? Model.ExpectedDate : LocalTime.Now.Date).ToString(""yyyy/MM/dd""))""")
                .Label(@"@(info.Type == ShipmentType.IntoUk ? ""Expected date of arrival"" : ""Expected date of departure"")");

            CustomField()
                .ControlMarkup("@info.Item.FirstBorderCrossing.DisplayValue")
                .VisibleIf("info.ShowFirstBorderCrossing")
                .Label("Border Crossing");

            Field(x => x.OfficeOfDestination)
           .Label("Office of Destination")
           .DataSource("((info.IsNCTSShipmentOutConvertible == true || (info.Type == ShipmentType.OutOfUk && info.Company.NCTS == NCTSType.Always))? await TransitOffice.NonGBDestinations : await TransitOffice.Destinations)")
           .AsAutoComplete()
           .DisplayExpression("item.DisplayValue")
           .Mandatory()
           .VisibleIf("info.ShowRoute && info.Type == ShipmentType.OutOfUk && info.IsNCTSShipmentOutConvertible == true")
           .AfterControlContainer("[#BUTTONS(AddSecondBorderCrossing)#]");


            Field(x => x.SecondBorderCrossing).VisibleIf("info.HasSecondBorder || info.Item?.SecondBorderCrossing != null && info.ShowExtraBoredrCrossing")
                .DataSource("await TransitOffice.Transits")
                .Label("Second Border Crossing")
                .AsAutoComplete()
                .DisplayExpression("item.DisplayValue")
                .AfterControlContainer("[#BUTTONS(AddThirdBorderCrossing)#]");

            Button("Add third border crossing").Text("Add Third Border Crossing").VisibleIf("!info.ThirdBorderCrossing_Visible  && info.ShowExtraBoredrCrossing")
               .Icon(FA.Plus).CssClass("float-left").CausesValidation(false)
               .OnClick(x =>
               {
                   x.CSharp("info.HasThirdBorder = true; info.ThirdBorderCrossing_Visible = true; return View(info);");
               });

            Field(x => x.ThirdBorderCrossing)
                .VisibleIf("info.HasThirdBorder || info.Item?.ThirdBorderCrossing != null  && info.ShowExtraBoredrCrossing")
                .DataSource("await TransitOffice.Transits")
                .Label("Third Border Crossing")
                .AsAutoComplete()
                .DisplayExpression("item.DisplayValue")
                .AfterControlContainer("[#BUTTONS(AddFourthBorderCrossing)#]");



            Button("Add fourth border crossing").Text("Add Fourth Border Crossing").VisibleIf("!info.FourthBorderCrossing_Visible  && info.ShowExtraBoredrCrossing")
              .Icon(FA.Plus).CssClass("float-left").CausesValidation(false)
              .OnClick(x =>
              {
                  x.CSharp("info.HasFourthBorder = true; info.FourthBorderCrossing_Visible = true; return View(info);");
              });

            Field(x => x.FourthBorderCrossing)
                .VisibleIf("info.HasFourthBorder || info.Item?.FourthBorderCrossing != null  && info.ShowExtraBoredrCrossing")
                .DataSource("await TransitOffice.Transits")
                .Label("Fourth Border Crossing")
                .AsAutoComplete()
                .DisplayExpression("item.DisplayValue");

            Field(x => x.UseAuthorisedLocation)
                .Mandatory()
                .AsRadioButtons(Arrange.Horizontal)
                .ReloadOnChange()
                .VisibleIf("info.Company != null && await info.Company.AuthorisedLocations.Any() && info.IsNCTSShipmentOutConvertible == true");

            Field(x => x.AuthorisedLocation).AsAutoComplete().Mandatory()
               .Label("Select authorised location")
               .DataSource("await info.Company.AuthorisedLocations")
               .VisibleIf("info.UseAuthorisedLocation == true && await info.Company.AuthorisedLocations.Count() > 1")
               .ReloadOnChange();

            CustomField("")
                .Label("Authorised location")
                .VisibleIf("info.UseAuthorisedLocation == true && await info.Company.AuthorisedLocations.Count() == 1")
                .ControlMarkup("@(await info.Company.AuthorisedLocations.Select(x => x.LocationName).FirstOrDefault())");

            var endBox = Box("Attachments", BoxTemplate.WrapperDiv);
            MasterDetail(x => x.UploadAttachments, a =>
            {
                a.Field(x => x.Attachment).NoLabel().CustomDataSave("info.UploadAttachments = info.UploadAttachments.Except(z => z.Item.Attachment.FileName == \"NoFile.Empty\").ToList();");
                a.Button("Add Another Attachment").CausesValidation(false).CssClass("add-button").OnClick(x => x.AddMasterDetailRow());
            }).InitialCardinality(1).Box(endBox);

            Button("Save and Add/Amend Consignments").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.RunInTransaction(false);
                x.CSharp("info.Item.AuthorisedLocation = await info.Company.AuthorisedLocations.FirstOrDefault();")
                    .If("info.UseAuthorisedLocation == true && await info.Company.AuthorisedLocations.Count() == 1");
                x.CSharp("info.Type = info.Item.Type;").If("!info.Item.IsNew");
                x.CSharp("info.Item.Company = info.Company;").If("info.Item.IsNew");
                x.CSharp(@"info.Item.PrimaryContact = info.PrimaryContact;")
                .If("info.Item.IsNew");
                x.CSharp("info.Item.SafetyAndSecurity = true;").If("info.AlwaysShowSafetyAndSecurity()");
                // x.CSharp("if(info.IsGVMS == true) info.Item.GVMSStatus = GVMSStatus.Pending;");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.Go<Share.Consignments.ConsignmentsPage>().Send("shipment", "info.Item.ID").If("await info.Item.Consignments.Count() > 0");
                x.Else().Go<Share.Consignments.ConsignmentEnterPage>().Send("shipment", "info.Item.ID").SendReturnUrl();
            });
          
            Button("Add contact").Icon(FA.Plus).CssClass("float-left").NoText().CausesValidation(false).OnClick(x =>
            {
                x.If("info.Company == null").MessageBox("Please select a Company before adding a new Contact.");
                x.Else().PopUp<Share.Utils.AddContactPage>().Send("company", "info.Company.ID");
            });

            Button("Add Carrier").CausesValidation(false).Icon(FA.Plus).CssClass("float-left").NoText().OnClick(x =>
            {
                x.PopUp<Admin.Carrier.AddCarrierPage>();
            });

            OnBeforeSave("saving expected date from custom control")
              .Code("item.ExpectedDate = Convert.ToDateTime(info.ExpectedDate);");

            OnBound("Set Selected primary Contact after Contact Popup")
               .Code(@"var newSelectedContact = await (GetFromSession(""primary_contactId_"" + User.GetId()).To<Guid>()).To<Contact>();
                        if(newSelectedContact != null){
                            info.PrimaryContact = newSelectedContact;
                        info.PrimaryContact_Text = newSelectedContact.ToStringOrEmpty();}");

            OnBound("Set Office of destincation")
               .Code(@"info.Item.FirstBorderCrossing = info.Route?.Non_UKPort?.TransitOffice ?? info.Item.FirstBorderCrossing;
                       if(!info.Item.IsNew) info.Type = info.Item.Type;");


            OnBound("Set Selected carrier after carrier Popup")
              .Code(@"var newSelectedCarrier = await (GetFromSession(""carrier_contactId_"" + User.GetId()).To<Guid>()).To<Carrier>();
                        if(newSelectedCarrier != null){
                            info.Carrier = newSelectedCarrier;
                        info.Carrier_Text = newSelectedCarrier.ToStringOrEmpty();}");



            ViewModelProperty<bool>("HasSecondBorder").RetainInPost().NotReadOnly();
            ViewModelProperty<bool>("HasThirdBorder").RetainInPost().NotReadOnly();
            ViewModelProperty<bool>("HasFourthBorder").RetainInPost().NotReadOnly();
            ViewModelProperty<bool>("IsChannelportUser").NotReadOnly()
                .OnBound(@"info.IsChannelportUser = User.IsInRole(""Admin"");");

            OnBound("load companyuser as company").Code(@"if(CompanyUser != null){info.Company = CompanyUser.Company;}");
            OnBound("Safety and Security").Code(@"if(info.AlwaysShowSafetyAndSecurity(info.Item.Type)){info.SafetyAndSecurity = true;
                info.Item.SafetyAndSecurity = true;}");
            OnBound("check ncts type").Code("if(info.SetNCTS){info.IsNCTSShipmentOutConvertible=true;}");
            OnBound("bound if only 1 AuthorisedLocation").Code(@"if(info.Company != null)
            {
                if (await info.Company?.AuthorisedLocations.Count() == 1)
                {
                    info.AuthorisedLocation = await info.Company.AuthorisedLocations.FirstOrDefault();
                }
            }
          ");

            OnJavascript("remove space").Code(@" 
                                           $(""#VehicleNumber"").keypress(function (e) {
                                                $(this).val($(this).val().replace("" "", ''));
                                            })
                                            $(""#TrailerNumber"").keypress(function (e) {
                                                $(this).val($(this).val().replace("" "", ''));
                                            })"
            );
        }
    }
}