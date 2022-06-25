using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipentNCTSOutOfUKASMAcc : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewTransitOfficePL, CreateNewCountry_France, AdminAddsCompanyGeeksQA, AddProductForGeeksQA, AddNewContactForGeeksQARafalQA, AddCompanyAPISettingsRaf, AddRoutePortsmouthToAmsterdam>();
            LoginAs<ChannelPortsAdmin>();

            Goto("/");


            ExpectHeader("Shipments");

            //---------creating shipmnet------

            Click("NCTS Shipments Out of UK");
            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TEST QA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TEST QA");
            Set("Customer reference").To("7596324");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");
            Set("Expected date of departure").To("10/02/2022");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth to AMSTERDAM");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Portsmouth to AMSTERDAM");

            ClickField("Office of Destination");
            Expect(What.Contains, "PL SZCZECIN PL987654 POLAND");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "PL SZCZECIN PL987654 POLAND");

            Click("Save and Add/Amend Consignments");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            Set("EAD MRN").To("12GB45678945612345");
            Click("Search");

            Set("UK Trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK Trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            Set("Partner name").To("");
            ClickHeader("Consignment Details");
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            Set("Country of destination").To("FRANCE");
            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("10000");
            Click("Save and Add Commodities");

            //-----add commodity-----

            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212 - 14");
            Set("Description of goods").To("Aliens");
            Set("Gross weight").To("1000");
            Set("Net weight").To("900");
            Set("Value").To("10000");
            Set("Number of packages for this commodity code (if known)").To("100");

            Click("Save");

            ExpectButton("Complete");
            ClickButton("Complete");

            //ClickButton("Complete");

            //ClickLink("Shipments");
            //Set("to").To("10/07/2022");
            //Click("Search");

            //AtRow("R0121000001").Column("Progress").Expect("Ready to Transmit");

            ////------transmit part-------

            //AtRow("R0121000001").Column("Tracking number").ClickLink();

            //AtRow("R012100000101").Column("Transmit").ClickButton("Transmit");

        }
    }
}
