using MSharp;

namespace Modules
{
    class RiseSupportTicket : FormModule<Domain.SupportTicket>
    {
        public RiseSupportTicket()
        {
            HeaderText("Raise Support Request");

            Field(x => x.User)
                 .Control(ControlType.AutoComplete)
                 .DataSource("(info.Shipment.Company != null ? await info.Shipment.Company?.GetAvailableContacts() : new List<Person>())")
                 .Label("Full Name")
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
                .ItemCssClass("readonly-field")
                .ControlMarkup("@info.Shipment?.TrackingNumber")
                .Label("Tracking Number")
                .VisibleIf("info.Shipment != null");

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


            ViewModelProperty<Domain.Shipment>("Shipment")
                .FromRequestParam("shipment");

            ViewModelProperty<Domain.User>("CreatedBy");

            Button("Cancel").OnClick(x => x.CloseModal());

            Button("save-others").Text("Save").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.CSharp(@"
                    info.Item.CreatedBy = Context.Current.User().ExtractUser<User>();
                    info.Item.Company =  info.Shipment.Company;
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
                x.CloseModal(Refresh.Full);

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



            ViewModelProperty<Domain.SupportTicket>("SupportTicket").OnBound(@"info.SupportTicket = await Database.Of<SupportTicket>().Where(x => x.Shipment == info.Shipment).FirstOrDefault();");

            ViewModelProperty<bool>("IsClosed")
                .OnBound(@"if(info.SupportTicket != null)
                             if(info.SupportTicket.Status == SupportTicketStatus.Closed)
                                info.IsClosed = true;");

            ViewModelProperty<bool>("IsExits")
                .OnBound(@"if(info.SupportTicket != null)
                             if(info.SupportTicket.Status != SupportTicketStatus.Closed)
                                info.IsExits = true;");

            OnBound("load data").Code(@"info.Item.Shipment = info.Shipment;");
            OnBound("load data").Code(@"info.Item.User = info.User == null ? info.Shipment.PrimaryContact : info.User;");
            OnBound("load data").Code(@"info.Item.User.MobileNumber = info.Item.User.MobileNumber.HasValue() ? info.Item.User.MobileNumber 
                                                                 : await Extensions.GetContactNumber(info.Item.User.ID);");


        }
    }
}