using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotUseURLToEditCommodityInSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to commodity details page
            LoginAs<ChannelPortsAdmin>();
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            AtRow(The.Top).Column("Edit").Click("Edit");
            WaitToSeeHeader("Commodity Details");
            Click("Save and Add/Amend Consignments");
            AtRow("R011900000101").Column("Edit").Click("Edit");
            Click("Save and Add Commodities");
            Click("Complete");
            Click("Back to Shipments");


            CopyUrl();

            //submit shipment
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            // AtRow(That.Contains, "R0119000001").Column("Submit").ClickButton();
            // WaitToSeeHeader("Shipments");

            //try to access edit commodity page via url
            GotoCopiedUrl();
            ExpectNoHeader("Commodity Details");
        }
    }
}
