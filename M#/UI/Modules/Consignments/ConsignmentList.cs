using MSharp;

namespace Modules
{
    class ConsignmentList : BaseListModule<Domain.Consignment>
    {
        public ConsignmentList()
        {
            HeaderText("Consignments - @(info.Shipment.Type == ShipmentType.IntoUk ? \"Into UK\":\"Out of UK\") [#BUTTONS(NewConsignment)#]");

            ViewModelProperty<Domain.Shipment>("Shipment")
                .FromRequestParam("shipment");

            DataSource("await info.Shipment.Consignments.GetList()");

            ButtonsLocation("Bottom");

            ShowFooterRow(true);

            Column(x => x.ConsignmentNumber);

            Column(x => x.UCR)
                .LabelText("Declaration Unique Consignment Reference (DUCR)");

            Column(x => x.UKTrader)
                .LabelText("UK Trader")
                .DisplayExpression("@item.UKTrader.Name");

            Column(x => x.Partner)
                .DisplayExpression("@item.Partner.Name");

            Column(x => x.Declarant)
                .DisplayExpression("@item.Declarant.Name");

            Column(x => x.TotalPackages);

            Column(x => x.TotalGrossWeight)
                .DisplayFormat("{0:#,#.##}")
                .FooterMarkup("<strong>@Model.Items.Sum(c => c.Item.TotalGrossWeight)</strong>");

            Column(x => x.TotalNetWeight)
                .DisplayFormat("{0:#,#.###}")
                .FooterMarkup("<strong>@Model.Items.Sum(c => c.Item.TotalNetWeight)</strong>");

            Column(x => x.InvoiceNumber);

            Column(x => x.InvoiceCurrency);

            Column(x => x.TotalValue)
                .DisplayFormat("{0:n}")
                .FooterMarkup("<strong>@(\"{0:n}\".FormatWith(Model.Items.Sum(c => c.Item.TotalValue)))</strong>");

            LinkColumn("Commodities")
                .HeaderText("Commodities")
                .Text(@"@await item.Commodities.GetList().Count()")
                .OnClick(x =>
                {
                    x.Go<Share.Commodities.CommoditiesPage>().Send("consignment", "item.ID").SendReturnUrl();
                });

            Column(x => x.AdminStatusLabel)
                .LabelText("Progress")
                .VisibleIf(AppRole.Admin);

            Column(x => x.CustomerStatusLabel)
                .LabelText("Progress")
                .VisibleIf(AppRole.Customer);

            ButtonColumn("Edit")
                .HeaderText("Actions")
                .GridColumnCssClass("actions-merge")
                .Icon(FA.Edit)
                .VisibleIf("@item.IsEditable(GetUser())")
                .OnClick(x =>
                {
                    x.CSharp("item.ResetStatus();");
                    x.Go<Share.Consignments.ConsignmentEnterPage>()
                    .Send("item", "item.ID").Send("shipment", "info.Shipment.ID")
                    .SendReturnUrl();
                });

            ButtonColumn("Delete")
               .HeaderText("Actions")
               .GridColumnCssClass("actions-merge")
               .ConfirmQuestion("Are you sure you want to delete this consignment?")
               .CssClass("btn-danger")
               .Icon(FA.Remove)
               .VisibleIf("item.CanBeDeleted")
               .OnClick(x =>
               {
                   x.DeleteItem();
                   x.Reload();
               });

            this.ArchiveButtonColumn("Consignment")
                .HeaderText("Actions")
                .GridColumnCssClass("actions-merge")
                .VisibleIf("!await item.Archive() || item.IsDeactivated");

            ButtonColumn("Archive")
               .HeaderText("Actions")
               .GridColumnCssClass("actions-merge")
               .VisibleIf("await item.Archive() && !item.IsDeactivated")
               .OnClick(x => x.PopUp<Share.Archive.ConsignmentArchivePage>()
               .Send("consignment", "item.ID"));


            Button("New Consignment")
                .IsDefault().Icon(FA.Plus)
                .CssClass("float-right")
                .OnClick(x =>
                {
                    x.Go<Share.Consignments.ConsignmentEnterPage>()
                    .Send("shipment", "info.Shipment.ID")
                    .SendReturnUrl();
                });

            Button("Back to Shipments").OnClick(x =>
            {
                x.Go<Admin.ShipmentPage>()
                .If(AppRole.Admin);

                x.Go<Customer.ShipmentsIntoUKPage>()
                .If(AppRole.Customer)
                .If("info.Shipment.IsInUK");

                x.Go<Customer.ShipmentsIntoUKPage>()
                .If(AppRole.Customer)
                .If("info.Shipment.IsOutUK");
            });

            ViewModelProperty<string>("CountryLabel");

            OnBound("Set Country Label")
                .Code(@"info.CountryLabel = info.Shipment.IsInUK ? ""Country of origin"":""Country of destination"";");
        }
    }
}