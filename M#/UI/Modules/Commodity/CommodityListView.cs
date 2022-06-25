using MSharp;

namespace Modules
{
    class CommodityListView : BaseListModule<Domain.Commodity>
    {
        public CommodityListView()
        {
            HeaderText("@info.Consignment.ConsignmentNumber - Commodities");
            ViewModelProperty<Domain.Consignment>("Consignment").FromRequestParam("consignment");
            DataSource("await info.Consignment.Commodities.OrderBy(t => t.SubmitDate).GetList()");
            ShowFooterRow(true);

            Column(x => x.Product).LabelText("Product");


            Column(x => x.GrossWeight).DisplayFormat("{0:#,0.##}")
                .FooterMarkup("Total: @(\"{0:#,0.##}\".FormatWith(await info.Consignment.GetCurrentGrossWeight()))");
            Column(x => x.NetWeight).DisplayFormat("{0:#,0.##}")
                .FooterMarkup("Total: @(\"{0:#,0.##}\".FormatWith(await info.Consignment.GetCurrentNetWeight()))");
           //CustomColumn("Currency").LabelText("Currency").DisplayExpression("@info.Consignment.InvoiceCurrency");
            Column(x => x.Value).DisplayFormat("{0:#,0.##}")
                .FooterMarkup("Total: @(\"{0:#,0.##}\".FormatWith(await info.Consignment.GetCurrentValue()))");
            Column(x => x.NumberOfPackages).LabelText("Number of packages").DisplayFormat("{0:#,0}")
                .FooterMarkup("Total: @(\"{0:#,0}\".FormatWith(await info.Consignment.GetCurrentNumberOfPackages()))");
            Column(x => x.CountryOfDestination).IsSortable(false)
                .LabelText("@(info.Consignment.Shipment.IsInUK ? \"Country of origin\":\"Country of destination\")");
            Column(x => x.HasPreference).LabelText("Preference")
                .DisplayExpression("@(item.HasPreference == true ? \"Yes\" : \"No\")");
        }
    }
}