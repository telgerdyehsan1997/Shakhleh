using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class PartnerGetsCreatedViaConsignmentPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";

            Run<AddShipmentForMajimaConstruction_IntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipment to add Consignment
            this.FindShipment(trackingNumber);
            AtRow(trackingNumber).Column("Consignments").ClickLink("0");

            ExpectLink("New Consignment");
            ClickLink("New Consignment");

            ExpectHeader("Consignment Details");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);

            //Adds the Partner via the Consignment page
            Click("AddPartnerCompany");
            ExpectHeader("Add Company");
            Set("Company name").To("Partner name");
            this.ClickAndWait("Country", "FRANCE");
            Set(That.Contains, "Postcode").To("FR01");
            Set("Address line 1").To("France Address");
            Set("Town/City").To("London");
            ClickButton("Save");

            //Assert that Partner has been created
            AtField("Partner name").Expect(What.Contains, "PARTNER NAME");
        }
    }
}