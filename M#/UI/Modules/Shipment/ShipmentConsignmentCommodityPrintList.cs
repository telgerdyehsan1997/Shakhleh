using System;
using System.Collections.Generic;
using System.Text;
using MSharp;

namespace Modules
{
    class ShipmentConsignmentCommodityPrintList : ListModule<Domain.Commodity>
    {
        public ShipmentConsignmentCommodityPrintList()
        {
            DataSource("await info.Consignment.Commodities.OrderBy(t => t.SubmitDate).GetList()");
            IsViewComponent();

            Column(x => x.Product.CommodityCode).LabelText("Commodity Code");
            Column(x => x.GrossWeight).DisplayFormat("{0:#,0.##} kg")
                .FooterMarkup("Total: @(\"{0:#,0.##} kg\".FormatWith(await info.Consignment.GetCurrentGrossWeight()))");
            Column(x => x.NetWeight).DisplayFormat("{0:#,0.##} kg")
                .FooterMarkup("Total: @(\"{0:#,0.##} kg\".FormatWith(await info.Consignment.GetCurrentNetWeight()))");
            CustomColumn("Currency").LabelText("Currency").DisplayExpression("@info.Consignment.InvoiceCurrency");
            Column(x => x.Value).DisplayFormat("{0:#,0.##}")
                .FooterMarkup("Total: @(\"{0:#,0.##}\".FormatWith(await info.Consignment.GetCurrentValue()))");
            Column(x => x.NumberOfPackages).LabelText("Number of packages").DisplayFormat("{0:#,0}")
                .FooterMarkup("Total: @(\"{0:#,0}\".FormatWith(await info.Consignment.GetCurrentNumberOfPackages()))");
            Column(x => x.CountryOfDestination).IsSortable(false)
                .LabelText("@(info.Consignment.Shipment.IsInUK ? \"Country of origin\":\"Country of destination\")");
            Column(x => x.HasPreference).LabelText("Preference")
                .DisplayExpression("@(item.HasPreference == true ? \"Yes\" : \"No\")");

            ViewModelProperty<Domain.Consignment>("Consignment")
                .RetainInPost();
        }
    }
}
