using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminEnablesEIDRForCFSPConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";

            Run<AdminCreatesShipmentWithCFSPCompany>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigates to Consignments
            AtRow(trackingNumber).Column("Consignments").ClickLink();
            ClickLink("New Consignment");

            //Asserts new 'Using EIDR' label
            ExpectHeader("Consignment Details");
            ClickHeader("Consignment Details");
            AtLabel("Do you have full Custom Declaration details?").ClickLabel("Yes");
            AtLabel("Do you wish to use EIDR").ClickLabel("Yes");
            ExpectLabel("Sequence number");

            //Sets the 'Sequence number' field
            Set("Sequence number").To("123Sequence456");
            ClickHeader("Consignment Details");

            //Asserts that the UCR displays the Company Authorisation number and the Sequence number
            //Format Authorisation number-Sequence number
            AtLabel("UCR").Expect("1GB683470514002-123SEQUENCE456");
        }
    }
}