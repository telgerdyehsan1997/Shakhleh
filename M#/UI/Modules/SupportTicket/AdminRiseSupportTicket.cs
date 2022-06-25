using MSharp;

namespace Modules
{
    class AdminRiseSupportTicket : FormModule<Domain.SupportTicket>
    {
        public AdminRiseSupportTicket()
        {
            HeaderText("Raise Support Request");

            Field(x => x.Company)
              .Control(ControlType.AutoComplete)
              .DataSource(@"await Database.Of<Company>()
                .Where(x => !x.IsDeactivated && !x.IsCreatedFromAPI && x.TypeId.IsAnyOf(CompanyType.Customer, CompanyType.Flex, CompanyType.Forwarder)).GetList()")
              .Label("Company Name")
              .Mandatory()
              .RequiredValidationMessage("The Company name field is required")
              .ReloadOnChange();


            Field(x => x.User)
                 .ChangeEventHandler(@"info.Item.User = info.User;
                                        info.Item.User.MobileNumber = info.User.MobileNumber = info.Item.User.MobileNumber.HasValue() ? info.Item.User.MobileNumber
                                            : await Extensions.GetContactNumber(info.Item.User.ID);")

                 .Control(ControlType.AutoComplete)
                 .DataSource("(info.Company != null ? await info.Company?.GetAvailableContacts() : new List<Person>())")
                 .Label("Full Name")
                 .RequiredValidationMessage("The User field is required.")
                 .ReloadOnChange();

            CustomField()
                .ItemCssClass("readonly-field")
                .ControlMarkup(@"@info.User?.Email")
                .Label("Email");


            var cc = MasterDetail(x => x.EmailCC, a =>
            {
                a.Field(x => x.EmailCc)
                .NoLabel();

                a.Button("Add Another")
                .NoText()
                .Icon(FA.Plus)
                .CausesValidation(false)
                .CssClass("add-button")
                .OnClick(x => x.AddMasterDetailRow());
            })
            .InitialCardinality(1)
            .Label("CC");

            CustomField()
                .ItemCssClass("readonly-field")
                .ControlMarkup(@"@info.User?.MobileNumber")
                .VisibleIf("info.User?.MobileNumber.HasValue() == true")
                .Label("Phone");

            CustomField()
                .PropertyType("string")
                .PropertyName("PhoneNumber")
                .RequiredValidationMessage("The Phone field is required")
                .Mandatory()   
                .VisibleIf("info.User?.MobileNumber.HasValue() == false")
                .Label("Phone");

            CustomField()
               .ReloadOnChange()
               .PropertyName("TrackingNumber")
               .PropertyType("string")
               .Label("Tracking Number");

            Field(x => x.TaskDetail)
                .ControlCssClass("persist-casing")
                .Label("Details")
                .Mandatory();

            var endBox = Box("Attachments", BoxTemplate.WrapperDiv);
            MasterDetail(x => x.Attachments, a =>
            {
                a.Field(x => x.Attachment)
                .NoLabel()
                .CustomDataSave("info.Attachments = info.Attachments.Except(z => z.Item.Attachment.FileName == \"NoFile.Empty\").ToList();");

                a.Button("Add Another Attachment")
                .CausesValidation(false)
                .CssClass("add-button")
                .OnClick(x => x.AddMasterDetailRow());
            })
            .InitialCardinality(1)
            .MaxCardinality(3)
            .Box(endBox);

            ViewModelProperty<Domain.Shipment>("Shipment");

            ViewModelProperty<Domain.User>("CreatedBy");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("save-others")
                .Text("Save")
                .IsDefault()
                .Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp(@"if(info.IsTrackingNumber)
                        return Notify(info.TrackingNumber + "" does not relate to the Company that is currently selected."", ""error"");
                ");
                x.CSharp(@"
                    info.Item.CreatedBy = Context.Current.User().ExtractUser<User>();
                ");
                x.CSharp(@"if(info.PhoneNumber.HasValue())
                {
                     if (!info.PhoneNumber.All(char.IsDigit))
                           return Notify(""The provided Mobile number is not a valid Integer text(digits only)."",""error"");

                   await Database.Update(info.User, x => x.MobileNumber = info.PhoneNumber);

                }");

                x.CSharp(@"foreach (var email in info.EmailCC)
                { 
                   if(email.EmailCc.HasValue())
                       if (!Helper.EmailIsValid(email.EmailCc))
                            return Notify(email.EmailCc + "" is not a valid Email address"");
                }");
                x.CSharp(@"info.EmailCC = info.EmailCC.Except(x => x.EmailCc.IsEmpty()).ToList();");
                x.SaveInDatabase();
                x.GentleMessage("Saved successfully.");
                x.ReturnToPreviousPage();

            }).VisibleIf("info.SupportTicket == null");

            Button("save-closed").Text("Save").IsDefault().Icon(FA.Check)
               .OnClick(x =>
               {

                   x.PopUp<Admin.Dashboard.SuppportTicketReOpenPage>()
                      .Send("supportTicket", "info.SupportTicket.ID");

               }).VisibleIf("info.IsClosed");

            Button("open-responce").Text("Save").IsDefault().Icon(FA.Check)
              .OnClick(x =>
              {
                  x.RunInTransaction();
                  x.CSharp(@"var item = new Response();
                     if(info.TaskDetail.HasValue())
                     {
                        item = await Database.Save(new Response
                        {
                            Sender = User.Identity.Name,
                            User = User.ExtractUser<User>(),
                            Message = info.TaskDetail,
                            SupportTicket = info.SupportTicket,

                        });
                        if (User.IsInRole(""Admin""))
                         {
                             var companyUser = await Database.Of<CompanyUser>().Where(x => x.CompanyId == info.SupportTicket.Company).GetList();
                             foreach (var company in companyUser)
                             {
                                 await Database.Save(new UserReponseNotification
                                 {
                                     User = company,
                                     Response = item,
                                     HasSeen = false
                                 });
                             }
                         }
                        foreach (var attachment in info.Attachments.Where(x => x.Attachment != null))
                        {
                            if (attachment.Attachment.Filename != ""NoFile.Empty"")
                            {
                               await Database.Save(new ResponseAttachment
                                    {
                                        Response = item,
                                        Attachment = attachment.Item.Attachment

                                    });
                            }
                    }
                }");

                  x.PopUp<Admin.Dashboard.SuppportTicketRedirectToResponsePage>()
                     .Send("supportTicket", "info.SupportTicket.ID")
                     .Send("response", "item.ID");

              }).VisibleIf("info.IsExits");


            ViewModelProperty<bool>("IsTrackingNumber").OnBound(@"if(info.TrackingNumber.HasValue())
            {
                const int maxLength = 11;
                info.TrackingNumber = info.TrackingNumber.Trim();
                if (info.TrackingNumber.Length > maxLength)
                 {
                     info.TrackingNumber =  info.TrackingNumber.Substring(0, info.TrackingNumber.Length-2);
                 }
                info.Shipment = await Database.Of<Shipment>().Where(x => x.TrackingNumber == info.TrackingNumber && x.Company == info.Company).FirstOrDefault();
                if(info.Shipment != null) { info.IsTrackingNumber = false; }
                else { info.IsTrackingNumber = true; }
            }");

            ViewModelProperty<Domain.SupportTicket>("SupportTicket")
                .OnBound(@"if(info.Shipment != null){ info.SupportTicket = await Database.Of<SupportTicket>().Where(x => x.Shipment == info.Shipment).FirstOrDefault();}");

            ViewModelProperty<bool>("IsClosed")
                .OnBound(@"if(info.SupportTicket != null)
                             if(info.SupportTicket.Status == SupportTicketStatus.Closed)
                                info.IsClosed = true;");

            ViewModelProperty<bool>("IsExits")
                .OnBound(@"if(info.SupportTicket != null)
                             if(info.SupportTicket.Status != SupportTicketStatus.Closed)
                                info.IsExits = true;");

            OnBound("load data").Code(@"info.Item.Shipment = info.Shipment;");
        }
    }
}