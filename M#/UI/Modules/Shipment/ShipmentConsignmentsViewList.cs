using MSharp;

namespace Modules
{
    class ShipmentConsignmentsViewList : BaseListModule<Domain.Consignment>
    {
        public ShipmentConsignmentsViewList()
        {
            HeaderText("Consignments - @(info.Shipment.Type == ShipmentType.IntoUk ? \"Into UK\":\"Out of UK\")");
            ViewModelProperty<Domain.Shipment>("Shipment").FromRequestParam("shipment");
            DataSource("await info.Shipment.Consignments.GetList()");
            ShowFooterRow(true);
            Inject("IEADShipmentService");

            Column(x => x.ConsignmentNumber);

            Column(x => x.UCR)
                .LabelText("Declaration Unique Consignment Reference (DUCR)");

            Column(x => x.UCN).VisibleIf("info.Shipment.IsInUK");

            Column(x => x.UKTrader).LabelText("UK Trader").DisplayExpression("@item.UKTrader.Name");
            Column(x => x.Partner).DisplayExpression("@item.Partner.Name");
            Column(x => x.Declarant).DisplayExpression("@item.Declarant.Name");
            Column(x => x.TotalPackages);
            Column(x => x.TotalGrossWeight).DisplayFormat("{0:#,#.##}").FooterMarkup("<strong>@Model.Items.Sum(c => c.Item.TotalGrossWeight)</strong>");
            Column(x => x.TotalNetWeight).DisplayFormat("{0:#,#.###}").FooterMarkup("<strong>@Model.Items.Sum(c => c.Item.TotalNetWeight)</strong>");
            Column(x => x.InvoiceNumber);
            Column(x => x.InvoiceCurrency);
            Column(x => x.EADMRN).LabelText("MRN").VisibleIf("info.Shipment.IsInUK");

            Column(x => x.ICSMRNNumber)
                .LabelText("ICS MRN")
                .VisibleIf("info.Shipment.SafetyAndSecurity == true && info.Shipment.IsInUK");

            Column(x => x.EntryReference)
                .LabelText("Entry Number");

            Column(x => x.EADMRN).LabelText("EAD Number")
                .VisibleIf("info.Shipment.IsOutUK");

            Column(x => x.TotalValue)
                .DisplayFormat("{0:n}")
                .GridColumnCssClass("value-column")
                .FooterMarkup("<strong>@(\"{0:n}\".FormatWith(Model.Items.Sum(c => c.Item.TotalValue)))</strong>");

            LinkColumn("Commodities")
                .HeaderText("Comm")
                .Text(@"@await item.Commodities.GetList().Count()")
                .OnClick(x =>
                {
                    x.Go<Share.Commodities.CommoditiesViewPage>()
                    .Send("consignment", "item.ID")
                    .SendReturnUrl();
                });

            LinkColumn(x => x.AdminStatusLabel)
                .HeaderText("Progress")
                .ColumnVisibleIf(AppRole.Admin)
                .OnClick(x => x.PopUp<Share.Shipment.ShipmentView.ManageProgressPage>()
                .Send("consignment", "item.ID"));

            LinkColumn(x => x.CustomerStatusLabel)
                .HeaderText("Progress")
                .ColumnVisibleIf(AppRole.Customer)
                .OnClick(x => x.PopUp<Share.Shipment.ShipmentView.ASMResponsePage>()
                .Send("consignment", "item.ID"));

            CustomColumn("EntryType")
              .LabelText("Entry Type")
              .DisplayExpression(@"@await item.EntryTypeText(item.Shipment.Route?.UKPort)")
              .VisibleIf("info.Shipment.Type == ShipmentType.IntoUk && info.Shipment.RouteId.HasValue");

            ButtonColumn("Manual Update Cleared")
                 .HeaderText("Actions")
                 .GridColumnCssClass("actions-merge")
                .VisibleIf("(GetUser() is ChannelPortsUser) && item.CanBeFlagedAsCleared")
                .OnClick(x =>
                {
                    x.ShowPleaseWait();
                    x.CSharp("await item.FlagAsCleared();");
                    x.RefreshPage();
                });

            ButtonColumn("Manual Update Arrived")
                 .HeaderText("Actions")
                 .GridColumnCssClass("actions-merge")
                .VisibleIf("(GetUser() is ChannelPortsUser) && item.CanBeFlagedAsArrived")
                .OnClick(x =>
                {
                    x.ShowPleaseWait();
                    x.CSharp("await item.FlagAsArrived();");
                    x.RefreshPage();
                });

            ButtonColumn("Manual Update With Customs")
                 .HeaderText("Actions")
                 .GridColumnCssClass("actions-merge")
                .VisibleIf("(GetUser() is ChannelPortsUser) && item.CanBeFlagedAsWithCustom")
             .OnClick(x =>
             {
                 x.ShowPleaseWait();
                 x.CSharp("await item.FlagAsWithCustoms();");
                 x.RefreshPage();
             });

            ButtonColumn("Mark as With Importer")
                 .HeaderText("Actions")
                 .GridColumnCssClass("actions-merge")
                 .VisibleIf("(GetUser() is ChannelPortsUser) && item.CanSendToCustom && await item.HasControl(item) && item.ProgressId == Progress.EntryControlled")
                 .OnClick(x =>
                 {
                     x.ShowPleaseWait();
                     x.CSharp("await IEADShipmentService.SendControlDeclaration(item);");
                     x.RefreshPage();
                 });


            LinkColumn("View Logs-@await item.Logs.GetList().Count()")
                 .HeaderText("Actions")
                 .GridColumnCssClass("actions-merge")
                .OnClick(x =>
                {
                    x.Go<Share.Consignments.ConsignmentLogsPage>().Send("consignment", "item.ID").SendReturnUrl();
                });

            ButtonColumn("Transmit to HRMC")
                .VisibleIf(AppRole.Admin)
                .HeaderText("Actions")
                .GridColumnCssClass("actions-merge")
                .VisibleIf("@item.CanBeTransmit")
                .ValidateAntiForgeryToken(false)
                .OnClick(x =>
                {
                    x.ShowPleaseWait();
                    x.CSharp(@"await EADShipmentService.Transmit(item);").ValidationError();
                    x.Reload();
                });

            LinkColumn(@"@(Model.Shipment.IsOutUK ? ""View EAD Document"" : ""View Import Entry"")")
                .HeaderText("Actions")
                .GridColumnCssClass("actions-merge")
                .OnClick(x =>
                {
                    x.Go<Share.Shipment.ShipmentView.ConsignmentDocumentsPage>().Send("consignment", "item.ID");
                });



            ButtonColumn("Edit")
                 .HeaderText("Actions")
                 .GridColumnCssClass("actions-merge")
                 .Icon(FA.Edit).VisibleIf("@item.IsEditable(GetUser())")
                  .OnClick(x =>
                  {
                      x.CSharp("item.ResetStatus();");
                      x.Go<Share.Consignments.ConsignmentEnterPage>().Send("item", "item.ID").Send("shipment", "info.Shipment.ID").SendReturnUrl();
                  });

            LinkColumn("View Tax Amount")
                 .HeaderText("Actions")
                 .GridColumnCssClass("actions-merge")
                 .VisibleIf("await item.IsDefermentSent()")
                  .OnClick(x =>
                  {
                      x.PopUp<Share.Shipment.ShipmentView.TaxAmountPage>().Send("item", "item.ID").SendReturnUrl();
                  });

            ButtonColumn("Change Entry Type")
              .HeaderText("Actions")
              .GridColumnCssClass("actions-merge")
              .VisibleIf("@info.Shipment.Type == ShipmentType.IntoUk && await item.CanChangeEntryType()")
              .OnClick(x =>
              {
                  x.PopUp<Share.Consignments.ConsignmentEntryTypePage>()
                  .Send("entryType", "await item.EntryTypeText(item.Shipment.Route.UKPort)")
                  .Send("consignment", "item.ID")
                  .SendReturnUrl();
              });


            ViewModelProperty<string>("CountryLabel");
            OnBound("Set Country Label")
                .Code(@"info.CountryLabel = info.Shipment.Type == ShipmentType.IntoUk ? ""Country of origin"":""Country of destination"";");
        }
    }
}