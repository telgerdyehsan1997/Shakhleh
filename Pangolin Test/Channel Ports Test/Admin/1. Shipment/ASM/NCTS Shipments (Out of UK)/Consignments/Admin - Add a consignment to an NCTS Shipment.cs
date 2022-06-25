using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddAConsignmentToAnNCTSShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddNewNCTSShipments_OutOfUK>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Set(The.Bottom, "to").To("10/07/2022");
            Click("Search");

            AtRow("1000000").Column("Edit").ClickLink("Edit");

            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");

            ExpectHeader("Consignment Details");

            Set("EAD MRN").To("12GB56789012345678");
            Click("Search");
            ClickField("Uk trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Spain");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Spain");

            Set("Total packages").To("10");
            Set("Total gross weight").To("60");
            Set("Total net weight").To("45");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "EUR");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "EUR");
            Set("Total value").To("50");

            Click(What.Contains, "Save and Add Commodities");

            ExpectHeader(That.Contains, "CP100000001 - Commodities");

        }
    }
}