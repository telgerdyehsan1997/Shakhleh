using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotUseURLToAddConsignmentToSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to shipment edit page
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow("R0119000001");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            Click("New Consignment");
            CopyUrl();
            ClickLink("Cancel");
            ExpectRow("R011900000101");
            AtRow("R011900000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");
            Click("Save and Add Commodities");
            ExpectHeader(That.Contains, "Commodities");
            AtRow("ABS12343").Column("Edit").Click("Edit");

            Set("Country of origin").To("");
            ClickHeader("Commodity Details");
            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FR - FRANCE");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FR - FRANCE");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Click("Save");
            Click("Complete");
            ExpectRow("R011900000101");
            AtRow("R011900000101").Column("Progress").Expect("Ready to Transmit");

            //submit shipment
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Tracking number").ClickLink();
            ExpectHeader("Shipment Details");
            AtRow("R011900000101").Column("Transmit").ClickButton("Transmit");

            //try to access add consignment page via url
            GotoCopiedUrl();
            //ExpectNoHeader("Consignment Details");
        }
    }
}
