using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCheckAndAddsFileUnderView : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewTransitOfficePL, AdminAddsCompanyGeeksQA, AddProductForGeeksQA, AddNewContactForGeeksQARafalQA, AddCompanyAPISettingsRaf, AddRoutePortsmouthToAmsterdam>();
            LoginAs<ChannelPortsAdmin>();


            Goto("/");

            ExpectHeader("Shipments");

            //---------creating shipmnet------

            Click("New Shipment");

            ClickField("Company name");
            Type("Geeks");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GEEKSQA");

            ClickLabel("Into UK");
            Set("Primary contact").To("Rafal QA");
            Set("Customer reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");
            Set("Port of arrival").To("Portsmouth");

            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("19/01/2022");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            ClearField("UK Trader");
            ClickField("UK Trader");
            Type("Geeks");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClearField("Partner name");
            ClickField("Partner name");
            Type("Shipping");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "SHIPPING COMPANY LTD - ROME - FG6 YFD - SC859485859485 - 1234567");

            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("2347890");
            Set("Invoice currency").To("GREAT BRITAIN - GBP");
            Set("Total value").To("10000");
            Set("Terms of sale").To("FAS");
            ClickLabel("Only 1 Commodity");
            Click("Save and Add Commodities");

            //-----add commodity-----

            ExpectHeader("Commodity Details");

            ClickField("Product code");
            Type("Aliens");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Aliens");

            Set("Second quantity").To("10");
            Set("Number of packages for this commodity code (if known)").To("100");

            ClickField("Country of origin");
            Type("ES");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "ES");

            Click("Save");

            ExpectButton("Complete");
            ClickButton("Complete");

            ClickLink("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("R0121000001").Column("Progress").Expect("Ready to Transmit");


        }
    }
}
