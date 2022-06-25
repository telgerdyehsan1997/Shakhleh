using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SendLRNToDriverNoCountryCodeSet : UITest
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

            //Adds the Number
            AtRow(trackingNumber).Column("Send LRN To Driver").ClickLink();
            ExpectHeader("Send LRN To Driver");
            Set("Mobile Number").To("07804659252");
            ClickButton("Save");

            //Attempts the send the message
            Expect(What.Contains, "The Country Code field is required");
        }
    }
}