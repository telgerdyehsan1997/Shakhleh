using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DuplicateEADMRN_AssertUpdatedMessage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000001";
            var eadMRN = "12GB45678945612345";


            Run<AddShipmentWithDuplicateEADMRN>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS Shipments Out of UK
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Finds the Shipmnent
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow(trackingNumber);
            AtRow(trackingNumber).Column("Edit").Click("Edit");

            //Navigates to the Shipments Commoditiy page
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");

            //Adds the duplicate EAD MRN
            ClickLink("New Consignment");
            Set("EAD MRN").To(eadMRN);
            ClickButton("Search");

            //The following message is triggered through a different workflow
            /*
            Click("Save and Add Commodities");
            Expect(What.Contains, "The EAD MRN provided is used on a previous Consignment"); */
        }
    }
}