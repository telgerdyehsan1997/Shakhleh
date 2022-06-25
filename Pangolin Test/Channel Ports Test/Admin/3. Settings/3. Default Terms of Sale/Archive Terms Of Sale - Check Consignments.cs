using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveTermsOfSale_CheckConsignments : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";
            var shipmentRoute = "CALAIS to Blackpool";
            var invoiceCurrency = "Great Britain - GBP";
            var termsOfSale = "FAS";


            Run<AddRouteBlackpoolAndCalais, CreateNewOfficeOfTransit_Italy, CreateNewTermsOfSale_EXO>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");
            ClickHeader("Shipment Details");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, shipmentRoute);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, shipmentRoute);

            Set("Customer Reference").To("CusRef1");
            Set("Vehicle number").To("67584");


            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("01/01/2023");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            ExpectHeader("Consignment Details");
            ClickHeader("Consignment Details");

            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            /*Set("Declarant").To("");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, consignmentDeclarant);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, consignmentDeclarant); */

            Set("Total packages").To("1");
            Set("Total gross weight").To("1");
            Set("Total net weight").To("1");
            Set("Invoice number").To("1");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, invoiceCurrency);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, invoiceCurrency);

            Set("Total value").To("1");

            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, termsOfSale);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, termsOfSale);

            Click("Save and Add Commodities");

            //Archives the terms of sale
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Default Terms of Sale");
            ExpectHeader("Default Terms of Sale");
            AtRow(termsOfSale).Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive reason");
            Click(The.Left, "Archive");

            ClickLink("Shipments");
            ExpectHeader("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            ExpectRow("R0721000001");
            AtRow("R0721000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            AtRow("R072100000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");

            AtLabel("Terms of Sale").Expect(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            ExpectNo(What.Contains, termsOfSale);
        }
    }
}