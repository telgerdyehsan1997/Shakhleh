using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminReleasesShipmentWithValueOver300000 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000000";
            var LRN = "CP100000001";

            Run<AddNewNCTSShipmentOut_CommodityValueOver300000>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS Shipments
            this.NavigateToNCTSShipments();

            //Finds the Shipment with value over 300000
            this.FindNCTSShipment(trackingNumber);

            //Releases the Shipment
            this.ReleaseNCTSShipment(trackingNumber, LRN);
        }
    }
}