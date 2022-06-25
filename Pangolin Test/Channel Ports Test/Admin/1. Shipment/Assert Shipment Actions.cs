using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertShipmentActions : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";

            Run<NewShipmentOutOfUK_SafetyAndSecurityEnabled>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Asserts that the 'Actions' column has been implemented
            AtRow(trackingNumber).AtColumn("Actions").ExpectXPath("/html/body/main/div[1]/div/div/form/div[3]/div/div[2]/div/div[14]/div/button");

            //Clicks on 'Select action' to check the Actions
            AtRow(trackingNumber).AtColumn("Actions").ClickXPath("/html/body/main/div[1]/div/div/form/div[3]/div/div[2]/div/div[14]/div/button");
            /*  Expect("Print");
              Expect("Edit");
              Expect("Archive"); */
        }
    }
}