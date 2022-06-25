using MSharp;

namespace Modules
{
    class SuppportTicketRedirectToResponse : FormModule<Domain.SupportTicket>
    {
        public SuppportTicketRedirectToResponse()
        {
            HeaderText("A Ticket for this Tracking Number already exists. Would you like to add a Response to the Ticket?");
            ViewModelProperty<Domain.SupportTicket>("SupportTicket").FromRequestParam("supportTicket");
            ViewModelProperty<Domain.Response>("Response").FromRequestParam("response");

            Button("No")
                 .OnClick(x =>
                 {
                     x.RunInTransaction();
                     x.CSharp(@"await Database.Delete(await Database.Of<UserReponseNotification>().Where(x => x.Response == info.Response).GetList());");
                     x.CSharp(@"await Database.Delete(await Database.Of<ResponseAttachment>().Where(x => x.Response == info.Response).GetList());");
                     x.CSharp(@"await Database.Delete(info.Response);");
                     x.CloseModal();
                 });

            Button("Yes")
                .OnClick(x =>
                {
                    x.CSharp("await Database.Update(info.Response,x=>x.IsConfirm = true);");
                    x.Go<Admin.Dashboard.SupportTicketResponseListPage>()
                    .Send("supportTicket", "info.SupportTicket.ID")
                    .SendReturnUrl();

                });
        }
    }
}