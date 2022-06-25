using MSharp;

namespace Modules
{
    class ShipmentFileErrorLogList : BaseListModule<Domain.ErrorLog>
    {
        public ShipmentFileErrorLogList()
        {
            HeaderText("Error Logs")
               .SortingStatement("item.RecievedDate DESC");

            CustomSearch("Customer Name")
           .ViewModelName("CustomerName")
           .ViewModelType("string")
           .Label("Customer Contact")
           .MemoryFilterCode(@"if(info.CustomerName.HasValue())
                                     { 
                                         result = result.Where(p => ((p.Shipment != null && p.Shipment.PrimaryContact.Name.ToUpper().Contains(info.CustomerName.ToUpper()))
                                                  || (p.NCTSShipmentOut != null && p.NCTSShipmentOut.PrimaryContact.Name.ToUpper().Contains(info.CustomerName.ToUpper()))));
                                    }");

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

            Search(x => x.Status)
               .DataSource("Database.GetList<SupportTicketStatus>()")
               .Control(ControlType.HorizontalRadioButtons)
               .Label("Status")
               .DefaultValueExpression("SupportTicketStatus.Active");

            SearchButton("Search")
                .Icon(FA.Search)
                .OnClick(x => x.ReturnView());



            //for customize row its will always hidden 
            Column(x => x.Action.Name)
                .GridColumnCssClass("action-highlight");

            LinkColumn(x => x.Shipment)
              .HeaderText("Tracking Number")
              .VisibleIf("item.Shipment != null")
              .OnClick(x => x.Go("/shipment-view", OpenIn.NewBrowserWindow)
              .Send("shipment", "item.Shipment.ID"));

            CustomColumn("Customer Name")
                .LabelText("Customer Contact")
                .DisplayExpression("@(item.NCTSShipmentOut != null ? item.NCTSShipmentOut.PrimaryContact.Name : item.Shipment.PrimaryContact.Name)");


            Column(x => x.Error)
                .LabelText("Task");

            Column(x => x.RecievedDate)
                .DisplayFormat("{0: dd/MM/yyyy HH:mm:ss}")
                .LabelText("Notification Time");

          
            Column(x => x.ClaimedBy)
                 .LabelText("Claimed By");

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

        }
    }
}