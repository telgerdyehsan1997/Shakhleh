using MSharp;
using Domain;
namespace Modules
{
    class SupportTicketDocumentList : BaseListModule<Domain.RiseTicketAttachment>
    {
        public SupportTicketDocumentList()
        {
            HeaderText("@info.SupportTicket.TicketNumber - Attachments");
            DataSource("await info.SupportTicket.Attachments.GetList()");
            this.AddDependency(typeof(IZipDownloadService));

            Column(x => x.Attachment)
                .DisplayExpression("@item.Attachment?.FileName")
                .LabelText("File Name");

            Column(x => x.Attachment)
                .LabelText("Download");

            ViewModelProperty<Domain.SupportTicket>("SupportTicket").FromRequestParam("supportTicket");
        }
    }
}