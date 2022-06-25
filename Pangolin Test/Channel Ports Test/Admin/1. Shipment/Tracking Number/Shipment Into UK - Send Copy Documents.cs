using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ShipmentIntoUK_SendCopyDocuments : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";

            Run<AdminUploadsE9ToShipment_IntoTheUK>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Tracking number").ClickLink();

            ExpectHeader("Shipment Details");

            //Sends the Copy Documents
            ClickLink("Send Copy Documents");

            ExpectHeader("Email Copy Documents");
            Expect("To enable the documents to be sent you will need to enter the declarants or UK Traders EORI number");
            Set("EORI number").To("GB683470514001");
            Set("Email address").To("KAZUMA.KIRYU@UAT.CO");
            ClickButton("Send");
            Expect("Copy of Entry Documents has been successfully sent.");
            ClickButton("OK");

            //Checks the mailbox to see if the Copy of the Entry Documents has been set
            CheckMailBox("");

            ExpectRow("Copy Entry Documents for reference INTOUK1, Tracking number R0721000001");
        }
    }
}