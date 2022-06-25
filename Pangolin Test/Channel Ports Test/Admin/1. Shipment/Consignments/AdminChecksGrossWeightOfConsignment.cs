using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminChecksGrossWeightOfConsignment : UITest
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
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "AMSTERDAM to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "AMSTERDAM to Portsmouth");
            ClickLabel("Into UK");
            Set("Primary contact").To("Rafal QA");
            Set("Customer reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");

            Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            Type("26/01/2022");
            System.Threading.Thread.Sleep(1000);
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

            Set("Total packages").To("100");
            Set("Total gross weight").To("31000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("2347890");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("10000");
            Set("Terms of sale").To("FAS - Free Alongside Ship");
            AtLabel("Only 1 Commodity").ClickLabel("Yes");
            Click("Save and Add Commodities");

            //-----add commodity-----

            ExpectHeader("Commodity Details");

            ClickHeader("Commodity Details");
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALIENS - ALI12343");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALIENS - ALI12343");

            Set("Second quantity").To("10");
            Set("Number of packages for this commodity code (if known)").To("100");

            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ES - Spain");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ES - Spain");

            Click("Save");

            ExpectButton("Complete");
            ClickButton("Complete");

            Expect("Please check the weight as it seems too high for a road vehicle.");
            Click("Cancel");

            System.Threading.Thread.Sleep(2000);
            ClickButton("Complete");
            Expect("Please check the weight as it seems too high for a road vehicle.");
            Click("Confirm");

            ExpectHeader("Duty is Payable on one or more of the commodity codes");
        }
    }
}
