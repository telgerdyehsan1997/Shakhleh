using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageTrackingNumberCommodities : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddConsignmentForWWL, AdminAddsProduct_IPad>();
            //navigate
            LoginAs<ChannelPortsAdmin>();

            ClickLabel(The.Top, "All");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("T0721000001").Column("Edit").Click("Edit");

            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            //edit consignment to set Partner as Truckers Ltd (in order to see and add iPad product as Commodity)
            AtRow("T072100000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");

            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Partner name").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Total packages").To("10");
            Set("Total gross weight").To("100");
            Set("Total net weight").To("90");
            Set("Invoice number").To("365987");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");

            Set("Total value").To("1000");
            Set("Terms of Sale").To("EXW - Ex Works");

            Click("Save and Add Commodities");

            //add new commodity

            Click("New Commodity");
            WaitToSeeHeader("Commodity Details");

            ClickHeader("Commodity Details");

            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "MEAT - MEA12343");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "MEAT - MEA12343");

            Set("Gross weight").To("12");
            Set("Net weight").To("9");
            Set("Second quantity").To("10");
            Set("Number of packages for this commodity code (if known)").To("10");
            AtLabel("Currency").Expect("Great Britain - GBP");
            Set("Value").To("749.99");

            //AtField("Country of destination");
            //Type("GB");
            //Click(What.Contains, "GB - United Kingdom");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GR - Greece");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");

            Click(What.Contains, "Save");

            ClickLink("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            ExpectRow("T0721000001");
        }
    }
}
