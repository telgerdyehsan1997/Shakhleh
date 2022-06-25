using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNCTSShipmentWithMultipleCommodities : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000000";
            var lrnNumber = "CP100000001";
            var firstCommodity = "12345678 - 14";
            var secondCommodity = "34545343453 - 14";

            Run<AddShipmentWithDuplicateEADMRN>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS Shipments Out of UK
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Sets the Shipment to be Draft so it can be edited
            AtRow(trackingNumber).Column("Tracking number").ClickLink();
            AtRow(lrnNumber).Column("Progress").Click("Ready to Transmit");
            Set(The.Top, "Progress").To("DraftNormal");
            ClickButton("Save");
            ClickLink("NCTS Shipments out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Finds the Shipmnent
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow(trackingNumber);

            //Navigates to the Shipments Commoditiy page
            AtRow(trackingNumber).Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            //ExpectHeader("Consignments");
            AtRow(lrnNumber).Column("Edit").Click("Edit");
            //ExpectHeader("Consignment Details");

            //Edits the Consignment Details to hold more Commodities
            Set("Total packages").To("102");
            Set("Total gross weight").To("1002");
            Set("Total net weight").To("902");
            Set("Total value").To("10002");
            Click("Save and Add Commodities");
            //The following workflow no longer occurs
            /*Expect(What.Contains, "The EAD MRN provided is used on a previous Consignment");
            ClickButton("OK"); */

            //Adds another Commodity
            Click("New Commodity");
            ExpectHeader("Commodity Details");
            ClickHeader("Commodity Details");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, firstCommodity);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, firstCommodity);
            Set("Description of goods").To("Another Commodity");
            Set("Gross weight").To("1");
            Set("Net weight").To("1");
            Set("Value").To("1");
            Set("Number of packages for this commodity code (if known)").To("1");
            Click("Save");

            //Adds a third Commodity
            Click("New Commodity");
            ExpectHeader("Commodity Details");
            ClickHeader("Commodity Details");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, secondCommodity);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, secondCommodity);
            Set("Description of goods").To("The Next Commodity");
            Set("Gross weight").To("1");
            Set("Net weight").To("1");
            Set("Value").To("1");
            Set("Number of packages for this commodity code (if known)").To("1");
            Click("Save");
            Click("Complete");
        }
    }
}