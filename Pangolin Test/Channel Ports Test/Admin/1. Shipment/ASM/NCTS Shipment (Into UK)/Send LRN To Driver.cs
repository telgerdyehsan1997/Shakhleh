using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SendLRNToDriver : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000000";

            Run<Admin_AddNewNCTSShipments_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Searches for the Shipment
            this.FindNCTSShipment(trackingNumber);

            //Sends the LRN to the Driver
            AtRow(trackingNumber).Column("Send LRN To Driver").ClickLink();
            ExpectHeader("Send LRN To Driver");
            ClickField("Country Code");
            Expect(What.Contains, "GB (+44)");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB (+44)");
            Set("Mobile Number").To("07804659252");
            ClickButton("Save");

            //Assert LRN has been sent to Driver
            AtRow(trackingNumber).Column("Send LRN To Driver").Expect("Send LRN To Driver: Sent 1");
        }
    }
}