using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentOutofUKwithNCTSticked : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRoutePortsmouthToAmsterdam, AdminAddsCompanyGeeksQA, AddProductForGeeksQA, AddNewContactForGeeksQARafalQA, CreateNewTransitOfficePL, AddCompanyAPISettingsRaf>();
            LoginAs<ChannelPortsAdmin>();

            AssumeDate("05/01/2022");
            Goto("/");


            ExpectHeader("Shipments");

            //---------creating shipmnet------

            Click("New Shipment");

            ClickField("Company name");
            Type("Geeks");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GEEKSQA");

            ClickLabel("Out of UK");
            AtLabel("NCTS").ClickLabel("Yes");

            Set("Primary contact").To("TEST QA");
            Set("Customer reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");
            Set("Expected date of departure").To("05/01/2022");

            ClickField("Route");
            Type("Port");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Portsmouth");

            ClickField("Office of destination");
            Type("PL");
            Click("PL SZCZECIN PL987654 POLAND");

            Click("Save and Add/Amend Consignments");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            ClearField("UK Trader");
            ClickField("UK Trader");
            Type("Geeks");
            Click(The.Bottom, What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClearField("Partner name");
            ClickField("Partner name");
            Type("Shipping");
            Click(The.Bottom, What.Contains, "SHIPPING COMPANY LTD - ROME - FG6 YFD - SC859485859485 - 1234567");

            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("2347890");
            Set("Invoice currency").To("GREAT BRITAIN - GBP");
            Click("GREAT BRITAIN - GBP");
            Set("Total value").To("10000");
            Set("Terms of sale").To("FAS - Free Alongside Ship");
            ClickLabel("Only 1 Commodity");
            Click("Save and Add Commodities");

            //-----add commodity-----

            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");

            ClickHeader("Commodity Details");
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALIENS - ALI12343");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALIENS - ALI12343");

            Set("Gross weight").To("1000");
            Set("Net weight").To("900");
            Set("Second quantity").To("10");
            Set("Value").To("10000");
            Set("Number of packages for this commodity code (if known)").To("100");

            ClickField("Country of destination");
            Type("ES");
            Click("ES - Spain");
            Click("Save");

            ExpectButton("Transmit");
            ClickButton("Transmit");

            ClickLink("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("T0122000001").Column("Progress").Expect("Ready to Transmit");

            //------transmit part-------
            AtRow("T0122000001").Column("Tracking number").ClickLink();
            AtRow("T012200000101").Column("Transmit").ClickButton("Transmit");
        }
    }
}
