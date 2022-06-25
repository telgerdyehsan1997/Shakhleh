using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminUploadsE9ToShipment_IntoTheUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<MajimaConstruction_TransmitShipmentIntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Tracking number").ClickLink();

            ExpectHeader("Shipment Details");

            //Uploads the Entry Document Zip File (E9)
            AtRow(consignmentNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            ClickLink("View Import Entry");

            ExpectHeader("R0721000001 - R072100000101 - Import Entry");
            Set("Choose file").To("Into The UK.zip");
            System.Threading.Thread.Sleep(2000);
            ClickButton("Save");

            //Runs the 'Process File Store background task
            this.ProcessFileStore();
            Goto("/");

            //Finds the shipment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Tracking number").ClickLink();

            ExpectHeader("Shipment Details");
            AtRow(consignmentNumber).Column("Entry Reference").Expect("060-000022E-02/05/2021");
        }
    }
}