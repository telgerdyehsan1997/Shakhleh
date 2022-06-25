using Pangolin;
using System;

namespace Channel_Ports_Test
{
    public static class NCTSShipmentHelper
    {
        public static void NavigateToNCTSShipments(this UITest @this)
        {
            @this.ClickLink("NCTS Shipments Out of UK");
            @this.ExpectHeader("NCTS Shipments Out of UK");
        }

        public static void FindNCTSShipment(this UITest @this, string trackingNumber)
        {
            @this.Set("Date Created").To("01/01/1999");
            @this.Set("Expected date of arrival/departure").To("01/01/1999");
            @this.Set(The.Top, "to").To("25/12/2025");
            @this.Set(The.Bottom, "to").To("25/12/2025");
            @this.ClickButton("Search");
            @this.Expect(trackingNumber);
        }

        public static void TransmitNCTSShipment(this UITest @this, string trackingNumber, string LRN)
        {
            @this.AtRow(trackingNumber).Column("Tracking number").ClickLink();
            @this.ExpectHeader("Shipment details");
            @this.AtRow(LRN).Column("Transmit").ClickButton("Transmit");
        }

        public static void ReleaseNCTSShipment(this UITest @this, string trackingNumber, string LRN)
        {
            @this.AtRow(trackingNumber).Column("Tracking number").ClickLink();
            @this.ExpectHeader("Shipment details");
            @this.AtRow(LRN).Column("Release due to value").ClickButton("Release");
        }
    }
}
