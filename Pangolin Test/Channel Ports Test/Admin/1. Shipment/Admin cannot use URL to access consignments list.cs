using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotUseURLToAccessConsignmentsList : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to shipment edit page
            LoginAs<ChannelPortsAdmin>();

            AssumeDate("01/02/2022");
            Goto("/");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            CopyUrl();

            Click("1");
            AtRow("ABS12343").Column("Edit").Click("Edit");
            Set("Country of origin").To("");
            ClickHeader("Commodity Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect("FR - FRANCE");
            System.Threading.Thread.Sleep(1000);
            Click("FR - FRANCE");

            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Click("Save");
            Click("Complete");

            Click("Shipments");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Progress").Expect("Ready to Transmit");

            //try to access add consignment page via url
            GotoCopiedUrl();
            ExpectHeader(That.Contains, "Consignments");
        }
    }
}
