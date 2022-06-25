using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotUseURLToAddCommodityToSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment,JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to commodity details page
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            Click("New Commodity");
            CopyUrl();

            //submit shipment
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            AtRow(That.Contains, "R0119000001").Column("Submit").ClickButton();
            WaitToSeeHeader("Shipments");

            //try to access add commodity page via url
            GotoCopiedUrl();
            ExpectNoHeader("Commodity Details");
        }
    }
}
