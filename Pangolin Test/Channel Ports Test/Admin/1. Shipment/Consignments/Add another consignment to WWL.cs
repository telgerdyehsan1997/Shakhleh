using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnotherConsignmentToWWL : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddConsignmentForWWL, AddProductForWWLMeat>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow(That.Contains, "T0721000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");

            ClickLink("New Consignment");

            Set("Total packages").To("3");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("WWL-2019-1101");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GREAT BRITAIN - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GREAT BRITAIN - GBP");
            Set("Total value").To("300");

            Set("Terms of Sale").To("EXW - EX Works");

            Set("UK Trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Partner name").To("");
            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");

            Click("Save and Add Commodities");
            Click("Back");
            Click("Cancel");

            WaitToSeeHeader(That.Contains, "Consignments");
            ExpectRow(That.Contains, "T072100000101");
        }
    }
}
