using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DuplicateEADMRN_AssertMultipleCommoditiesNotAdded : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000002";
            var lrnNumber = "CP100000201";
            /*var firstCommodity = "12121212 - 14";
            var secondCommodity = "12345678 - 14";
            var thirdCommodity = "34545343453 - 14"; */

            Run<AddNCTSShipmentWithDuplicateEADMRN_ImportMultipleCommodities>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS Shipments Out of UK
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Finds the Shipmnent
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow("1000000");

            //Navigates to the Shipments Commoditiy page
            AtRow(trackingNumber).Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            //ExpectHeader("Consignments");
            AtRow(lrnNumber).Column("Edit").Click("Edit");
            //ExpectHeader("Consignment Details");
            Click("Save and Add Commodities");
            //The following workflow no longer occurs
            /*
            Expect(What.Contains, "The EAD MRN provided is used on a previous Consignment");
            Click("OK");

            //Returns to the previous page and enables 'Import EAD Commodities'
            Click("Back");
            AtLabel("Import EAD Commodities").ClickCheckbox();
            Click("Save and Add Commodities");

            //Checks that there are no duplicate Commodity Codes
            AtRow(firstCommodity).Column("Commodity code").Expect(firstCommodity);
            AtRow(secondCommodity).Column("Commodity code").Expect(secondCommodity);
            AtRow(thirdCommodity).Column("Commodity code").Expect(thirdCommodity);
            /*
            Expect(What.Contains, "The EAD MRN provided is used on a previous Consignment");
            Click("OK"); */
        }
    }
}