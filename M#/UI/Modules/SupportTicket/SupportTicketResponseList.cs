using MSharp;

namespace Modules
{
    class SupportTicketResponseList : BaseListModule<Domain.Response>
    {
        public SupportTicketResponseList()
        {
            HeaderText("@(info.SupportTicket.Shipment != null ? info.SupportTicket.Shipment.TrackingNumber : info.SupportTicket.NCTSShipment?.TrackingNumber.ToString()) Responses")
                .SourceCriteria("item.SupportTicket == info.SupportTicket && item.IsConfirm == true")
                .SortingStatement("item.Date DESC");


            Column(x => x.Date)
                .LabelText("Date/Time");

            Column(x => x.Sender)
                .DisplayExpression("@item.User.Name");

            Column(x => x.Message)
              .GridColumnCssClass("wrap-text");

            Column(x => x.Attachments).DisplayExpression(@"@{
                 foreach (var attachment in await item.Attachments.GetList())
                 { 
                    <a href=""@attachment.Attachment"" target=""_blank"">Download</a><br/>
                 }
            }");

            ViewModelProperty<Domain.SupportTicket>("SupportTicket")
                .FromRequestParam("supportTicket");

            Button("New Response")
                .IsDefault()
                .Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Dashboard.SupportTicketResponsePage>()
                .Send("supportTicket", "info.SupportTicket.ID"))
                .VisibleIf("info.SupportTicket.Status != SupportTicketStatus.Closed");

        }
    }
}