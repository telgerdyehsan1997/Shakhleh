using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SendLRNToDriverAssertThatSameNumberCannotBeUsedTwice : UITest
    {
        [Ignore]
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

            //Adds the First Number
            AtRow(trackingNumber).Column("Send LRN To Driver").ClickLink();
            ExpectHeader("Send LRN To Driver");
            ClickField("Country Code");
            Expect(What.Contains, "GB (+44)");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB (+44)");
            Set("Mobile Number").To("07804659252");

            //Adds the Second Number
            ClickButton("Add Another");
            ClickField(The.Bottom, "Country Code");
            Expect(The.Bottom, "GB (+44)");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, "GB (+44)");
            Set(The.Bottom, "Mobile Number").To("07804659252");

            //Attempts the send the message
            ClickButton("Save");
            Expect(What.Contains, "The same Mobile Number cannot be used Twice"); //Validation message not confirmed
        }
    }
}