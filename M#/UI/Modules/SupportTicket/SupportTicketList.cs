using Domain;
using MSharp;

namespace Modules
{
    class SupportTicketList : BaseListModule<SupportTicket>
    {
        public SupportTicketList()
        {
            HeaderText("Support Tickets")
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

            Search(x => x.User.Name)
                  .Label("Customer Contact");

            Search(x => x.Status)
              .DataSource("Database.GetList<SupportTicketStatus>()")
              .Control(ControlType.HorizontalRadioButtons)
              .Label("Status")
              .DefaultValueExpression("SupportTicketStatus.Active");

            SearchButton("Search").Icon(FA.Search)
                       .OnClick(x =>
                       {
                           x.ReturnView();

                       }).IsDefault(true);

            //for customize row its will always hidden 
            Column(x => x.Action.Name)
                .GridColumnCssClass("action-highlight");


            LinkColumn(x => x.Shipment)
             .HeaderText("Tracking Number")
             .VisibleIf("item.Shipment != null")
             .OnClick(x => x.Go("/shipment-view", OpenIn.NewBrowserWindow)
             .Send("shipment", "item.Shipment.ID"));

            Column(x => x.TicketNumber);


            Column(x => x.User)
                 .LabelText("Customer Contact")
                 .DisplayExpression("@item.User.Name");

            Column(x => x.User)
                .LabelText("Customer Email")
                .DisplayExpression("@item.User.Email");

            LinkColumn(x => x.EmailCC)
              .HeaderText("CC")
              .Text("@await item.EmailCC.Count()")
              .OnClick(x =>
              {
                  x.PopUp<Admin.Dashboard.AmendCCPage>()
                      .Send("item", "item.ID")
                      .SendReturnUrl();
              });


            Column(x => x.User.MobileNumber)
                  .LabelText("Customer Phone")
                  .DisplayExpression("@(item.User.MobileNumber.HasValue() ? item.User.MobileNumber : await Extensions.GetContactNumber(item.User.ID))");


            Column(x => x.TaskDetail)
                .GridColumnCssClass("wrap-text")
                .LabelText("Task");

            Column(x => x.NotificationTime)
                 .LabelText("Notification Time");

            Column(x => x.ClaimedBy)
                 .LabelText("Claimed By");


            ButtonColumn("Attachments - @await item.Attachments.Count()")
             .HeaderText("Actions")
             .GridColumnCssClass("actions-merge")
             .OnClick(x =>
             {
                 x.Go<Admin.Dashboard.SupportTicketDocumentPage>()
                     .Send("supportTicket", "item.ID")
                     .SendReturnUrl();

             });

            ButtonColumn("Responses - @await item.Responses.Where(r => r.SupportTicketId == item).Count()")
              .HeaderText("Actions")
             .GridColumnCssClass("actions-merge").OnClick(x =>
                {
                    x.Go<Admin.Dashboard.SupportTicketResponseListPage>()
                    .Send("supportTicket", "item.ID")
                    .SendReturnUrl();
                });

            ButtonColumn("Claim")
               .HeaderText("Actions")
               .GridColumnCssClass("actions-merge")
               .OnClick(x =>
               {
                   x.CSharp(@"await Database.Update(item, x =>
                        {
                          x.Action = SupportTicketAction.Claim;
                          x.ClaimedBy = Context.Current.User().Identity.Name;
                       });
                   ");
                   x.RefreshPage();

               })
               .VisibleIf("item.Action == SupportTicketAction.Unclaim && item.Action != SupportTicketAction.Close");

            ButtonColumn("Unclaim")
              .HeaderText("Actions")
              .GridColumnCssClass("actions-merge")
              .OnClick(x =>
              {
                  x.CSharp(@"await Database.Update(item, x =>
                        {
                          x.ClaimedBy = null;
                          x.Action = SupportTicketAction.Unclaim;
                       });
                   ");
                  x.RefreshPage();

              }).VisibleIf("item.Action != SupportTicketAction.Unclaim && item.Action != SupportTicketAction.Close");

            ButtonColumn("Place on Hold")
              .HeaderText("Actions")
              .GridColumnCssClass("actions-merge")
              .OnClick(x =>
              {
                  x.CSharp(@"await Database.Update(item, x =>
                        {
                          x.Action = SupportTicketAction.PlaceOnHold;
                          x.ClaimedBy = Context.Current.User().Identity.Name + "" - On Hold"";
                       });
                   ");

                  x.RefreshPage();

              }).VisibleIf("item.Action != SupportTicketAction.Unclaim && item.Action != SupportTicketAction.Close");

            ButtonColumn("Highlight to Supervisor")
              .HeaderText("Actions")
              .GridColumnCssClass("")
              .OnClick(x =>
              {
                  x.CSharp(@"await Database.Update(item, x =>
                        {
                          x.Action = SupportTicketAction.HighlightToSupervisor;
                       });
                   ");
                  x.RefreshPage();
              })
              .VisibleIf("item.Action != SupportTicketAction.Unclaim && item.Action != SupportTicketAction.Close");

            ButtonColumn("Respond")
              .HeaderText("Actions")
              .GridColumnCssClass("actions-merge")
              .OnClick(x =>
              {
                  x.PopUp<Admin.Dashboard.SupportTicketResponsePage>()
                   .Send("supportTicket", "item.ID");

              }).VisibleIf("item.Action != SupportTicketAction.Unclaim && item.Action != SupportTicketAction.Close");

            ButtonColumn("Close")
             .HeaderText("Actions")
             .GridColumnCssClass("actions-merge")
             .OnClick(x =>
             {
                 x.CSharp(@"await Database.Update(item, x =>
                        {
                          x.Status = SupportTicketStatus.Closed;
                          x.Action = SupportTicketAction.Close;
                       });
                   ");
                 x.CSharp("await EmailTemplate.ClosedTickets(item);");
                 x.RefreshPage();

             }).VisibleIf("item.Action != SupportTicketAction.Unclaim && item.Action != SupportTicketAction.Close");

            ButtonColumn("Re-Open")
           .HeaderText("Actions")
           .GridColumnCssClass("actions-merge")
           .OnClick(x =>
           {
               x.CSharp(@"await Database.Update(item, x =>
                        {
                          x.Status = SupportTicketStatus.Active;
                          x.Action = SupportTicketAction.Claim;
                       });
                   ");
               x.RefreshPage();

           }).VisibleIf("item.Action == SupportTicketAction.Close && item.Status == SupportTicketStatus.Closed");

            Button("Raise Support Request")
               .Icon(FA.Plus)
               .IsDefault(false)
               .OnClick(x => x.Go<Admin.Dashboard.AdminRiseSupportTicketPage>()
               .SendReturnUrl());

            Button("Broadcast Message")
               .IsDefault(false)
               .OnClick(x => x.Go<Admin.Company.BoardcastMessagePage>()
               .SendReturnUrl());
        }
    }
}