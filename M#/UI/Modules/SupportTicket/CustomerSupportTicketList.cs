using Domain;
using MSharp;

namespace Modules
{
    class CustomerSupportTicketList : BaseListModule<SupportTicket>
    {
        public CustomerSupportTicketList()
        {
            HeaderText("Dashboard").
              SourceCriteria("item.Company == CompanyUser.Company")
               .SortingStatement("item.NotificationTime DESC");


            Search(x => x.Shipment.TrackingNumber)
                .DatabaseFilterCode(@"const int maxLength = 11;
                    if (info.Shipment_TrackingNumber.HasValue())
                    {
                        info.Shipment_TrackingNumber = info.Shipment_TrackingNumber.Trim();
                        if (info.Shipment_TrackingNumber.Length > maxLength)
                        {
                            info.Shipment_TrackingNumber =  info.Shipment_TrackingNumber.Substring(0, info.Shipment_TrackingNumber.Length-2);
                        }
                        DatabaseFilters.Add(n => n.Shipment.TrackingNumber.Contains(info.Shipment_TrackingNumber, false));
                    }
                ")
                .Label("Tracking Number");

            Search(x => x.TicketNumber);

            Search(x => x.Status)
                .DataSource("Database.GetList<SupportTicketStatus>()")  
                .Control(ControlType.HorizontalRadioButtons)
                .Label("Status")
                .DefaultValueExpression("SupportTicketStatus.Active");

            SearchButton("Search")
                .Icon(FA.Search)
                .OnClick(x => x.ReturnView());


            LinkColumn(x => x.Shipment)
            .CssClass(@"c#:await Helper.HasBeenHighlighted(item) ? ""action-highlighted"":""""")
            .HeaderText("Tracking Number")
            .VisibleIf("item.Shipment != null")
            .OnClick(x => x.Go("/shipment-view", OpenIn.NewBrowserWindow)
            .Send("shipment", "item.Shipment.ID"));


            Column(x => x.TicketNumber);

            Column(x => x.TaskDetail)
                .GridColumnCssClass("wrap-text")
                .LabelText("Task");

            LinkColumn(x => x.EmailCC)
               .HeaderText("CC")
               .Text("@await item.EmailCC.Count()")
               .OnClick(x =>
               {
                   x.PopUp<Admin.Dashboard.AmendCCPage>()
                       .Send("item", "item.ID")
                       .SendReturnUrl();
               });


            Column(x => x.NotificationTime)
                 .LabelText("Notification Time");

            LinkColumn("Attachments - @await item.Attachments.Count()")
            .HeaderText("Attachments")
            .OnClick(x =>
            {
                x.Go<Admin.Dashboard.SupportTicketDocumentPage>()
                    .Send("supportTicket", "item.ID")
                    .SendReturnUrl();

            });

            LinkColumn(x => x.Responses)
                .Text("@await item.Responses.Where(r => r.SupportTicketId == item).Count()")
                 .OnClick(x =>
                 {
                     x.Go<Admin.Dashboard.SupportTicketResponseListPage>()
                     .Send("supportTicket", "item.ID")
                     .SendReturnUrl();
                 });

            Column(x => x.Status)
                .LabelText("Status")
                .VisibleIf(AppRole.Customer);


            Button("Raise Support Request")
                .IsDefault(false)
                .Icon(FA.Plus)
                .OnClick(x => x.PopUp<Admin.Dashboard.CustomerRiseSupportTicketFromDashBoardPage>());
        }
    }
}