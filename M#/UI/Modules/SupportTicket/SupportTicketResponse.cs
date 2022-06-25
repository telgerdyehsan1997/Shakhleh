using MSharp;

namespace Modules
{
    class SupportTicketResponse : FormModule<Domain.Response>
    {
        public SupportTicketResponse()
        {
            HeaderText("Response details");

            Field(x => x.Message)
               .ControlCssClass("persist-casing")
               .Mandatory();

            var endBox = Box("Attachments", BoxTemplate.WrapperDiv);
            MasterDetail(x => x.Attachments, a =>
            {
                a.Field(x => x.Attachment).NoLabel().CustomDataSave("info.Attachments = info.Attachments.Except(z => z.Item.Attachment.FileName == \"NoFile.Empty\").ToList();");
                a.Button("Add Another Attachment").CausesValidation(false).CssClass("add-button").OnClick(x => x.AddMasterDetailRow());
            })
            .InitialCardinality(1)
            .MaxCardinality(3)
            .Box(endBox);

            AutoSet(x => x.IsConfirm).Value("true");

            ViewModelProperty<Domain.SupportTicket>("SupportTicket").FromRequestParam("supportTicket");
            ViewModelProperty<Domain.User>("User");

            Button("Cancel").OnClick(x => x.ReturnToPreviousPage());

            Button("Send").IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.CSharp(@"if((info.SupportTicket.User as User) != null)
                    {
                        await Database.Save(new UserReponseNotification
                        {
                            User = info.SupportTicket.User as User,
                            Response = info.Item,
                            HasSeen = false
                        });
                    }");
                x.GentleMessage("Saved successfully.");
                x.CloseModal(Refresh.Full);
            });

            OnBound("load data").Code(@"info.Item.User = info.User = Context.Current.User().ExtractUser<User>();");
           
        }
    }
}