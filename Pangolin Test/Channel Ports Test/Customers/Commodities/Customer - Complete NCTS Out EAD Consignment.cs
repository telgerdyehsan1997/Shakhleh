using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Customer_CompleteNCTSOutEADConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddEADMRNKnownConsignmentToNCTSOutOfUKShipment>();

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Progress").Expect("Draft");
            AtRow("1000000").Column("Edit").Expect("Edit");
            AtRow("1000000").Column("Tracking number").ClickLink();

            ExpectHeader(That.Contains, "Shipment Details");

            ExpectRow("CP100000001");
            AtRow("CP100000001").Column("Progress").Expect("Draft");
            AtRow("CP100000001").Column("Transmit to HMRC").ExpectNo("Transmit");

            Click("NCTS Shipments Out of UK");

            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Consignments").ClickLink();

            ExpectHeader(That.Contains, "Consignments");

            AtRow("CP100000001").Column("Commodities").ClickLink();

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            ExpectButton("Complete");
            ClickButton("Back");

            ExpectHeader(That.Contains, "Consignments");

            AtRow("CP100000001").Column("Progress").Expect("DraftNormal");
            AtRow("CP100000001").Column("Progress").ExpectNo("Ready to transmit");

            Click("NCTS Shipments Out of UK");

            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Progress").Expect("Draft");
            AtRow("1000000").Column("Progress").ExpectNo("In Progress");
            AtRow("1000000").Column("Edit").Expect("Edit");

            AtRow("1000000").Column("Consignments").ClickLink();

            ExpectHeader(That.Contains, "Consignments");

            AtRow("CP100000001").Column("Commodities").ClickLink();
            Click("Complete");

            ExpectHeader(That.Contains, "Consignments");
            AtRow("CP100000001").Column("Progress").Expect("ReadyToTransmit");
            Click("Back to Shipments");

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Tracking number").ClickLink();
            ExpectHeader(That.Contains, "Shipment Details");
            AtRow("CP100000001").Column("Logs").Expect("0");
            AtRow("CP100000001").Click("Transmit");
            // AtRow("CP100000001").Column("Logs").Expect("2");
            AtRow("CP100000001").Column("Logs").ClickLink();

            ExpectHeader("Logs");
        }
    }
}