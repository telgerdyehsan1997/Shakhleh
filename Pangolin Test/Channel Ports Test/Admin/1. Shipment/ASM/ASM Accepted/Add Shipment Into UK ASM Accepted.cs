using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentIntoUKASMAccepted : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyGeeksQA, AddProductForGeeksQA, AddNewContactForGeeksQARafalQA, AddCompanyAPISettingsRaf, AddRoutePortsmouthToAmsterdam>();
            LoginAs<ChannelPortsAdmin>();

            Goto("/");

            ExpectHeader("Shipments");

            //---------creating shipmnet------

            Click("New Shipment");

            ClickField("Company name");
            Type("Geeks");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GEEKSQA");
            System.Threading.Thread.Sleep(1000);
            ClickLabel("Into UK");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "AMSTERDAM to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "AMSTERDAM to Portsmouth");

            Set("Primary contact").To("Rafal QA");
            Set("Customer reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");
            /*ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom"); */
            Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            Type("19/01/2023");
            Click("Save and Add/Amend Consignments");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK Trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("2347890");

            ClickField("Invoice currency");
            Expect(What.Contains, "Great Britain - GBP");
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("10000");

            Set("Terms of Sale").To("FAS - Free Alongside Ship");

            ClickLabel("Only 1 Commodity");
            Click("Save and Add Commodities");

            //-----add commodity-----

            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Aliens");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Aliens");

            Set("Gross weight").To("1000");
            Set("Net weight").To("900");
            Set("Second quantity").To("10");
            Set("Value").To("10000");
            Set("Number of packages for this commodity code (if known)").To("100");

            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Greece");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Greece");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");

            Click("Save");

            ExpectButton("Complete");
            ClickButton("Complete");

            ClickLink("Shipments");
            Set("Date created").To("01/07/2021");
            Set(The.Top, "to").To("25/12/2025");
            Click("Search");

            AtRow("R0721000001").Column("Progress").Expect("Ready to Transmit");

            //------transmit part-------

            AtRow("R0721000001").Column("Tracking number").ClickLink();

            AtRow("R072100000101").Column("Transmit").ClickButton("Transmit");
        }
    }
}
