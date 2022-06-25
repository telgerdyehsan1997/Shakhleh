using MSharp;

namespace Modules
{
    class ShipmentConsignmentPrintList : ListModule<Domain.Consignment>
    {
        public ShipmentConsignmentPrintList()
        {

            HeaderText("Consignments - @(info.Shipment.Type == ShipmentType.IntoUk ? \"Into UK\":\"Out of UK\")");
            ViewModelProperty<Domain.Shipment>("Shipment").FromRequestParam("shipment");
            DataSource("await info.Shipment.Consignments.GetList()");
            ShowFooterRow(true);
            RenderMode(ListRenderMode.List);

            ViewModelProperty<ShipmentConsignmentCommodityPrintList>("LstCommodities").PerListItem();

            AfterDataRowMarkup($@"<tr><td colspan=14>{Reference<ShipmentConsignmentCommodityPrintList>().UsedPerItem().ViewModelName("LstCommodities").Ref}</td></tr>");

            Markup(@"<table class=""grid"">
            <thead>
                <tr>
                    <th>Consignment number</th>
                    <th>Declaration Unique Consignment Reference (DUCR)</th>
                    <th>UK Trader</th>
                    <th>UK Trader Address</th>
                    <th>UK Trader EORI</th>
                    <th>Partner</th>
                    <th>Partner Address</th>
                    <th>Declarant</th>
                    <th>Total packages</th>
                    <th>Total gross weight</th>
                    <th>Total net weight</th>
                    <th>Invoice number</th>
                    <th>Invoice currency</th>
                    @if (info.Shipment.IsInUK)
                    {
                        <th>MRN</th>
                    }
                    <th>Entry Reference</th>
                    <th class=""value-column"">Total value</th>
                    @if (User.IsInRole(""Admin""))
                    {
                        <th>Progress</th>
                    }
                    @if (User.IsInRole(""Customer""))
                    {
                        <th>Progress</th>
                    }
                </tr>
            </thead>
            <tbody>

                <tr>
                    <td>@item.ConsignmentNumber</td>
                    <td>@item.UCR</td>
                    <td>@item.UKTrader?.Name</td>
                    <td>@item.UKTrader?.Address,@item.UKTrader?.AddressStreet</td>
                    <td>@item.UKTrader?.EORINumber</td>
                    <td>@item.Partner?.Name</td>
                    <td>@item.Partner?.Address,,@item.Partner.AddressStreet</td>
                    <td>@item.Declarant.Name</td>
                    <td>@item.TotalPackages</td>
                    <td>@(string.Format(""{0:#,#.##} kg"", item.TotalGrossWeight))</td>
                    <td>@(string.Format(""{0:#,#.###} kg"", item.TotalNetWeight))</td>
                    <td>@item.InvoiceNumber</td>
                    <td>@item.InvoiceCurrency</td>
                    @if (info.Shipment.IsInUK)
                    {
                        <td>@item.EADMRN</td>
                    }
                    <td>@item.EntryReference</td>
                    <td class=""value-column"">@(string.Format(""{0:n}"", item.TotalValue))</td>
                    @if (User.IsInRole(""Admin""))
                    {
                        <td>@item.AdminStatusLabel</td>
                    }
                    @if (User.IsInRole(""Customer""))
                    {
                        <td>@item.CustomerStatusLabel</td>
                    }
                </tr>
                <tr>
                    <td colspan=14>
                        @(await Component.InvokeAsync
                  <ShipmentConsignmentCommodityPrintList>
                     (listItem.LstCommodities))
                    </td>
                </tr>
            </tbody>
        </table>");
            OnPostBound("Set Sub List")
                .Code("info.Items.Do(item => " +
                "{" +
                "item.LstCommodities = new vm.ShipmentConsignmentCommodityPrintList();" +
                "item.LstCommodities.Consignment = item.Item;" +
                "});");
                
            ViewModelProperty<string>("CountryLabel");
            OnBound("Set Country Label").Code(@"info.CountryLabel = info.Shipment.Type == ShipmentType.IntoUk ? ""Country of origin"":""Country of destination"";");
            
        }
    }
}