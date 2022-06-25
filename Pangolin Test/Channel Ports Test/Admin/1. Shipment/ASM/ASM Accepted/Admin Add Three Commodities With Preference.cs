using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddThreeCommoditiesWithPreference : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyGeeksQA, AddProductForGeeksQA, AddNewContactForGeeksQA_TestContact, AddRouteBlackpoolAndCalais>();
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
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CALAIS to Blackpool");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Blackpool");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TEST QA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TEST QA");
            Set("Customer reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");
            Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            Type("03/02/2022");
            Click("Save and Add/Amend Consignments");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClearField("Partner name");
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");

            ClearField("Declarant");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("2347890");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("10000");
            Set("Terms of sale").To("FAS - Free Alongside Ship");
            Click("Save and Add Commodities");

            //----- add 1st commodity-----

            ClickLink("New Commodity");
            ClickField("Product code");
            Type("Aliens");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "ALIENS");
            Set("Gross weight").To("100");
            Set("Net weight").To("90");
            Set("Value").To("100");
            Set("Second quantity").To("2");
            Set("Number of packages for this commodity code (if known)").To("100");
            ClickField("Country of origin");
            Type("GR");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GREECE");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Click("Save");

            //---2nd commodity---

            ClickLink("New Commodity");
            ClickField("Product code");
            Type("Worms");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "WORMS");
            Set("Gross weight").To("100");
            Set("Net weight").To("90");
            Set("Value").To("100");
            Set("Second quantity").To("2");
            Set("Number of packages for this commodity code (if known)").To("100");
            ClickField("Country of origin");
            Type("GR");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GREECE");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Click("Save");

            //-----add 3rd commodity-----

            ClickLink("New Commodity");
            ClickField("Product code");
            Type("Aliens");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "ALIENS");
            Set("Gross weight").To("100");
            Set("Net weight").To("90");
            Set("Value").To("666");
            Set("Second quantity").To("2");
            Set("Number of packages for this commodity code (if known)").To("100");
            ClickField("Country of origin");
            Type("GR");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GREECE");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Click("Save");

            ExpectRow("ALI12343");
            ExpectRow("RAF12343");
        }
    }
}
