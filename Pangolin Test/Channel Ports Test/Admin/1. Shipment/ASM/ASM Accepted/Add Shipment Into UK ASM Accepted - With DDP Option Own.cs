using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentIntoUKASMAcceptedWithDDPOptionOwn : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewTermsOfSale_EXO_DDP, AdminAddsCompanyGeeksQA, AddCompany_CFSPSetToOwn, AddNewContactForCFSPOwnTestRafalQA, AddProductForGeeksQAWithCommodityCodeAndEuQuota, AddCompanyAPISettingsRaf, AddRoutePortsmouthToAmsterdam, AddVatRate_Test>();
            LoginAs<ChannelPortsAdmin>();

            AssumeDate(DateTime.Parse("01/01/2023"));

            Goto("/");

            ExpectHeader("Shipments");

            //---------creating shipmnet------

            Click("New Shipment");

            ClickField("Company name");
            Type("CFSP");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "CFSP OWN TEST");
            System.Threading.Thread.Sleep(1000);
            ClickLabel("Into UK");

            System.Threading.Thread.Sleep(1000);
            ClickField("Route");
            //System.Threading.Thread.Sleep(1000);
            //Expect(What.Contains, "AMSTERDAM to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "AMSTERDAM to Portsmouth");

            System.Threading.Thread.Sleep(1000);
            Set("Safety and security").To("No");
            System.Threading.Thread.Sleep(1000);
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

            System.Threading.Thread.Sleep(1000);
            Set("UK trader").To("");
            System.Threading.Thread.Sleep(1000);
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK Trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            System.Threading.Thread.Sleep(1000);
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");

            System.Threading.Thread.Sleep(1000);
            Set("Declarant").To("");
            System.Threading.Thread.Sleep(1000);
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            System.Threading.Thread.Sleep(1000);
            Set("Total packages").To("100");
            System.Threading.Thread.Sleep(1000);
            Set("Total gross weight").To("1000");
            System.Threading.Thread.Sleep(1000);
            Set("Total net weight").To("900");
            System.Threading.Thread.Sleep(1000);
            Set("Invoice number").To("2347890");

            System.Threading.Thread.Sleep(1000);
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Set("Total value").To("10000");

            System.Threading.Thread.Sleep(1000);
            Set("Terms of Sale").To("EXO - EXO LTD");

            System.Threading.Thread.Sleep(1000);
            Set("Freight currency").To("Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Set("Freight amount").To("10");
            System.Threading.Thread.Sleep(1000);
            Set("DDP options").To("Duty and VAT Inclusive");

            System.Threading.Thread.Sleep(1000);
            ClickLabel("Only 1 Commodity");
            System.Threading.Thread.Sleep(1000);
            Click("Save and Add Commodities");

            //-----add commodity-----

            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Aliens");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Aliens");

            System.Threading.Thread.Sleep(1000);
            Set("Gross weight").To("1000");
            Set("Net weight").To("900");
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
            Set("Date created").To("01/01/2021");
            Set(The.Top, "to").To("25/12/2025");
            Click("Search");

            AtRow("R0123000001").Column("Progress").Expect("Manual - Quota");

            //------transmit part-------

            AtRow("R0123000001").Column("Tracking number").ClickLink();

            AtRow("R012300000101").Column("Actions").Click("Select action");
            Click("Transmit to HRMC");
            ExpectHeader("Shipment Details");
        }
    }
}
