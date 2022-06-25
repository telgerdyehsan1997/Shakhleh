using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddA2ndConsignmentToAnNCTSShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {


            Run<AddCompanyAlpha, AddCompanyDelta, Admin_AddNewNCTSShipments_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            Click("Search");

            AtRow("1000000").Column("Edit").ClickLink();

            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");

            Set("EAD MRN").To("56GB78541254688521");
            Click("Search");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click("ALPHA LTD - WORCESTER - WR5 3DA - GB123456782012 - 7654321");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FRANCE");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FRANCE");

            Set("Total packages").To("5");
            Set("Total gross weight").To("50");
            Set("Total net weight").To("37");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "EUR");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "EUR");
            Set("Total value").To("4000");

            Click("Save and Add Commodities");

            ClickLink("NCTS Shipments Out of UK");

            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Company name").Expect("Truckers ltd");
            AtRow("1000000").Column("Consignments").ClickLink();

            AtRow("CP100000001").Column("Commodities").ClickLink();
            ClickLink("New Commodity");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212 - 14");

            Set("Description of goods").To("Test product");
            Set("Gross weight").To("50");
            Set("Net weight").To("37");
            Set("Value").To("4000");
            Set("Number of packages for this commodity code (if known)").To("5");
            Click("Save");

            ExpectButton("Complete");
            ClickButton("Complete");

            AtRow("CP100000001").Column("Progress").Expect("ReadyToTransmit");
            Click("Back to Shipments");

            ExpectHeader("NCTS Shipments Out of UK");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow("1000000").Column("Progress").Expect("Ready to Transmit");









        }
    }
}
