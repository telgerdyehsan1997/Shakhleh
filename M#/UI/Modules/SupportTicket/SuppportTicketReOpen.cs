using MSharp;

namespace Modules
{
    class SuppportTicketReOpen : FormModule<Domain.SupportTicket>
    {
        public SuppportTicketReOpen()
        {
            HeaderText("A Closed Ticket for this Tracking number already Exists. Would you like to Re-open this ticket to add a Response?");
            ViewModelProperty<Domain.SupportTicket>("SupportTicket").FromRequestParam("supportTicket");

            Button("No")
                 .OnClick(x =>x.CloseModal());  

            Button("Yes")
                .OnClick(x =>
                {
                    x.CSharp(@"await Database.Update(info.SupportTicket, x => {
                                                                   x.Status = SupportTicketStatus.Active;
                                                                   x.Action = SupportTicketAction.Claim;
                                                                 });");

                    x.Go<Admin.Dashboard.SupportTicketResponseListPage>()
                    .Send("supportTicket", "info.SupportTicket.ID")
                    .SendReturnUrl();

                });
        }
    }
}