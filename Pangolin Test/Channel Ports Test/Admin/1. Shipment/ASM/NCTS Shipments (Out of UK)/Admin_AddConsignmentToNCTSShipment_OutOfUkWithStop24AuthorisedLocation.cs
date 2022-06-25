using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddConsignmentToNCTSShipment_OutOfUkWithStop24AuthorisedLocation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRouteSouthamptonAndValencia, AddNewContactForTruckers_AlanSmith, AddNewContactGroup_Import, AddStop24AsTruckersAuthorisedLocation, OfficeOfTransitES>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALAN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALAN SMITH");

            Set("Customer Reference").To("41222");
            Set("Vehicle number").To("2223");
            Set("Trailer number").To("4242");
            //Set("Driver mobile country").To("FR (+33)");
            //Set("Driver mobile number").To("7912345678");
            ClickHeader("Shipment details");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Southampton to VALENCIA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Southampton to VALENCIA");

            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ES MADRID ES001111 SPAIN");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ES MADRID ES001111 SPAIN");

            AtLabel("Use authorised location").ClickLabel("Yes");
            AtLabel("Authorised location").Expect("Stop 24");

            Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            Type("25/12/2025");
            System.Threading.Thread.Sleep(1000);
            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");

            Set("EAD MRN").To("12GB19283746574832");
            ClickButton("Search");

            ClickField("UK trader");
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
            Expect(What.Contains, "United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "United Kingdom");

            Set("Total packages").To("10");
            Set("Total gross weight").To("145");
            Set("Total net weight").To("75");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "EUR");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "EUR");
            Set("Total value").To("2000");

            Click("Save and Add Commodities");

            Click("New Commodity");

            ExpectHeader("Commodity Details");
            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212 - 14");
            Set("Description of goods").To("Heavy goods");
            Set("Gross weight").To("145");
            Set("Net weight").To("75");
            AtLabel("Currency").Expect("EUR");
            Set("Value").To("2000");
            Set("Number of packages for this commodity code (if known)").To("10");
            Click("Save");

            Click("Complete");

            ExpectHeader(That.Contains, "Consignments");

            AtRow("CP100000001").Column("UK Trader").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Partner").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Guarantor").Expect("Truckers ltd");
            AtRow("CP100000001").Column("Country of destination").Expect("GB");
            AtRow("CP100000001").Column("Total packages").Expect("10");
            AtRow("CP100000001").Column("Total gross weight").Expect("145 kg");
            AtRow("CP100000001").Column("Total net weight").Expect("75 kg");
            AtRow("CP100000001").Column("Invoice currency").Expect("EUR");
            AtRow("CP100000001").Column("Total value").Expect("2,000.00");
            AtRow("CP100000001").Column("Commodities").Expect("1");

            AtRow("CP100000001").Column("Commodities").Click("1");
            AtRow("12121212 - 14").Column("Edit").Expect("Edit");
            AtRow("12121212 - 14").Column("Delete").Expect("Delete");
        }
    }
}
