using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentForWWL : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddShipmentForWWL, AddProductForWWLMeat>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            ClickLabel(The.Top, "All");

            Set("Date created").To("01/01/2020");
            Set("Expected date of arrival/departure").To("01/01/2020");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "Worldwide").Column("Consignments").ClickLink();
            ClickLink("New Consignment");


            //add new con
            ExpectHeader("Consignment Details");

            Set("UK trader").To("");
            System.Threading.Thread.Sleep(1000);
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Declarant").To("");
            System.Threading.Thread.Sleep(1000);
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Total packages").To("4");
            Set("Total gross weight").To("10.25");
            Set("Total net weight").To("5.78");
            Set("Invoice number").To("WWL-2019-1101");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GBP");

            Set("Total value").To("1200");

            Set("Terms of Sale").To("EXW - Ex Works");

            Click("Save and Add Commodities");
            ExpectLabel("Consignment total gross weight");
        }
    }
}
