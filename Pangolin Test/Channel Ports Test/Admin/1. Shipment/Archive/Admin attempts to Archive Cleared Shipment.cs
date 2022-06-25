using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAttemptsToArchiveClearedShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";

            Run<UpdateShipmentIntoUK_Cleared>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Progress").Expect("Cleared");

            //Archives the Shipment
            AtRow(trackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Expect("Archive");
            System.Threading.Thread.Sleep(1000);
            Click("Archive");

            //Asserts Archive validation
            Expect("Are you sure you wish to archive this shipment as the entry has been arrived with Customs");
            Click("Yes");

            //Archives the Shipment and adds a Replcement tracking number
            Set("Please explain why").To("Archive reason");
            Set(The.Right, "Please enter a replacement Tracking number").To("R0219000001");
            ClickButton("Archive");

            //Assert that shipment has been archived 
            ExpectNoRow(trackingNumber);
            AtLabel("Status").ClickLabel("Archived");
            ClickButton("Search");
            ExpectRow(trackingNumber);
        }
    }
}