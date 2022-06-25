using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_EditConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddCompanyAlpha, AddCompanyDelta, Admin_AddAConsignmentToAnNCTSShipment>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Expected date of arrival/departure").To("01/07/2019");
            Set(The.Bottom, "to").To("17/07/2022");
            ClickButton("Search");

            AtRow("1000000").Column("Edit").ClickLink();
            ExpectHeader("Shipment Details");

            Click("Save and Add/Amend Consignments");

            AtRow("CP100000001").Column("Edit").ClickLink();

            Set("Partner name").To("");
            ClickHeader("Consignment Details");
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");

            Set("Total packages").To("15");
            Set("Total gross weight").To("80");
            Set("Total net weight").To("50");
            Set("Total value").To("40000");

            Click("Save and Add Commodities");

            Click("Back");

            Click("Cancel");

            AtRow("CP100000001").Column("UK Trader").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Partner").Expect("Alpha ltd");
            AtRow("CP100000001").Column("Guarantor").Expect("TRUCKERS LTD");
            AtRow("CP100000001").Column("Country of destination").Expect("ES");
            AtRow("CP100000001").Column("Total packages").Expect("15");
            AtRow("CP100000001").Column("Total gross weight").Expect("80 kg");
            AtRow("CP100000001").Column("Total net weight").Expect("50 kg");
            AtRow("CP100000001").Column("Invoice currency").Expect("EUR");
            AtRow("CP100000001").Column("Total value").Expect("40,000.00");
            AtRow("CP100000001").Column("Commodities").Expect("0");
        }
    }
}
