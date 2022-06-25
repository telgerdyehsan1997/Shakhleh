using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsAnotherShipmentWithinSameMonthForWWL_OutOfUK : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]
        [TestCategory("File Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCountry_France, AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny, AddCompanyUserForWWLRichardSmith, JennySmithAddsShipmentForWWL_OutOfUK>();
            LoginAs<JennySmithCustomer>();

            AssumeDate("15/02/2020");
            Goto("/");

            Click(The.Top, "Shipments Out of UK");
            WaitToSeeHeader("Shipments Out of UK");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");
            Set("Primary contact").To("Richard Smith");
            Click(The.Bottom, "Richard Smith");
            Set("Customer Reference").To("23456");
            ClickLabel("Out of UK");
            Set("Vehicle number").To("43210");
            Set("Trailer number").To("56789");
            Set("Expected date of departure").To("15/02/2020");
            Set(The.Top, "Choose file").To("Example.png");
            Click("Add another attachment");
            Set(The.Bottom, "Choose file").To("Example.png");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Blackpool to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Blackpool to CALAIS");

            /*ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom"); */

            /*
            ClickField("Port of departure");
            Type("Por");
            System.Threading.Thread.Sleep(1000);
            Click("Portsmouth"); */

            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");

            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Declarant").To("");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Total packages").To("10");
            Set("Total gross weight").To("100");
            Set("Total net weight").To("90");
            Set("Invoice number").To("222222");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            Set("Total value").To("100");

            AtLabel("Terms of sale").Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FCA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FCA");

            Click("Save and Add Commodities");

            Click("Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Bottom, "to").To("25/12/2022"); Set(The.Bottom, "to").To("25/12/2022");
            Set(The.Top, "to").To("25/12/2022");
            ClickButton("Search");

            ExpectRow("T0222000001");
            //AtRow("T0222000001").Column("Date").Expect("01/02/2022");
            AtRow("T0222000001").Column("Customer Reference").Expect("12345");
            AtRow("T0222000001").Column("Company name").Expect("Worldwide Logistics Ltd");
            AtRow("T0222000001").Column("Vehicle number").Expect("43210");
            AtRow("T0222000001").Column("Trailer number").Expect("56789");
            AtRow("T0222000001").Column("Progress").Expect("Draft");
            AtRow("T0222000001").Column("Weights mismatch").ExpectNoTick();
        }
    }
}
