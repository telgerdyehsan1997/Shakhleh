using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_CompleteNCTSOutConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<Admin_AddAConsignmentToAnNCTSShipment, Admin_AddACommodity, Admin_AddA2ndCommodity, Admin_EditACommodity>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("1000000").Column("Progress").Expect("Draft - Normal");
            AtRow("1000000").Column("Edit").Expect("Edit");
            AtRow("1000000").Column("Tracking number").ClickLink();

            ExpectHeader(That.Contains, "Shipment Details");

            ExpectRow("CP100000001");
            AtRow("CP100000001").Column("Progress").Expect("Draft - Normal");
            AtRow("CP100000001").Column("Transmit to HMRC").ExpectNo("Transmit");

            Click("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("1000000").Column("Consignments").ClickLink();

            ExpectHeader(That.Contains, "Consignments");

            AtRow("12GB56789012345678").Column("Progress").Expect("DraftNormal");
            AtRow("12GB56789012345678").Column("Commodities").ClickLink();

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            ExpectRow("12345678 - 12");
            ExpectRow("12121212 - 14");

            BelowRow("12121212 - 14").Column("Gross weight").Expect("Total: 40 kg");
            BelowRow("12121212 - 14").Column("Net weight").Expect("Total: 20 kg");
            BelowRow("12121212 - 14").Column("Value").Expect("Total: 50");
            BelowRow("12121212 - 14").Column("Number of packages").Expect("Total: 0");

            ExpectButton("Complete");
            ClickButton("Complete");

            Expect(What.Contains, "The total net weight for consignment is 45 kg and the total net weight for the commodities within this consignment is 20 kg. These values do not match.");
            Click("OK");

            AtRow("12345678 - 12").Click("Edit");

            ExpectHeader(That.Contains, "Commodity Details");

            Set("Gross weight").To("40");
            Set("Net weight").To("32");
            Click("Save");

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

            AtRow("12345678 - 12").Column("Gross weight").Expect("40 kg");
            AtRow("12345678 - 12").Column("Net weight").Expect("32 kg");
            BelowRow("12121212 - 14").Column("Gross weight").Expect("Total: 60 kg");
            BelowRow("12121212 - 14").Column("Net weight").Expect("Total: 45 kg");

            ClickButton("Complete");

            Expect(What.Contains, "The Total Packages for Consignment must equal the sum of all packages in the commodities of this consignment.");
            Click("OK");

            AtRow("12345678 - 12").Click("Edit");

            ExpectHeader(That.Contains, "Commodity Details");

            Set("Number of packages for this commodity code (if known)").To("10");
            Click("Save");

            ExpectHeader(That.Contains, "CP100000001 - Commodities");
            AtRow("12345678 - 12").Column("Number of packages").Expect("10");
            BelowRow("12121212 - 14").Column("Number of packages").Expect("Total: 10");

            ClickButton("Complete");

            ExpectHeader(That.Contains, "Consignments");

            AtRow("12GB56789012345678").Column("Progress").ExpectNo("Draft");
            AtRow("12GB56789012345678").Column("Progress").Expect("ReadyToTransmit");

            Click("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("1000000").Column("Progress").ExpectNo("Draft");
            AtRow("1000000").Column("Progress").Expect("Ready to Transmit");
            AtRow("1000000").Column("Edit").ExpectNo("Edit");

            AtRow("1000000").Column("Tracking number").ClickLink();

            ExpectHeader(That.Contains, "Shipment Details");

            AtRow("CP100000001").Column("Progress").Expect("Ready to Transmit");
            AtRow("CP100000001").Column("Transmit to HMRC").Expect("Transmit");

        }
    }
}
