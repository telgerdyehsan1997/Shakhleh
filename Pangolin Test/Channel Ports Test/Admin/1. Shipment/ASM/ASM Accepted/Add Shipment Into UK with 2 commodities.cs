using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentIntoUKWith2Commodities : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyGeeksQA, AddProductForGeeksQA, AddNewContactForGeeksQARafalQA, AddCompanyAPISettingsRaf, AddRoutePortsmouthToAmsterdam>();
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

            ClickLabel("Into UK");
            Set("Primary contact").To("Rafal QA");
            Set("Customer reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");
            Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            Type("06/01/2022");
            Click("Save and Add/Amend Consignments");

            ClickHeader("Shipment Details");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "AMSTERDAM to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "AMSTERDAM to Portsmouth");
            Click("Save and Add/Amend Consignments");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            Set("UK Trader").To("");
            ClickField("UK Trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "SHIPPING COMPANY LTD - ROME - FG6 YFD - SC859485859485 - 1234567");

            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("2347890");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect("Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click("Great Britain - GBP");

            Set("Total value").To("10000");
            AtLabel("Terms of Sale").Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FAS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FAS");
            Click("Save and Add Commodities");

            ExpectText(That.Contains, "Commodities");
            ClickLink("New Commodity");

            //-----add first commodity-----

            ExpectHeader("Commodity Details");

            ClickField("Product code");
            Type("Aliens");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Aliens");
            Set("Gross weight").To("500");
            Set("Net weight").To("450");
            Set("Value").To("5000");
            Set("Second quantity").To("10");
            Set("Number of packages for this commodity code (if known)").To("50");
            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");

            Click("Save");

            //-----add second commodity-----
            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");

            ClickField("Product code");
            Type("Worms");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Worms");
            Set("Gross weight").To("500");
            Set("Net weight").To("450");
            Set("Second quantity").To("10");
            Set("Value").To("5000");
            Set("Number of packages for this commodity code (if known)").To("50");
            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");

            Click("Save");

            ExpectRow(That.Contains, "ALI12343");
            ExpectRow(That.Contains, "RAF12343");

            ClickButton("Complete");

            AtRow("R012200000101").Column("Progress").Expect("Ready to Transmit");



        }
    }
}
