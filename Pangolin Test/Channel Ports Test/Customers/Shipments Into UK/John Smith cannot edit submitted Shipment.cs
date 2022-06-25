using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCannotEditSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //submit shipment
            LoginAs<JohnSmithCustomer>();
            ExpectHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            ExpectRow("R011900000101");
            AtRow("R011900000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");
            ClickButton("Save and Add Commodities");

            AtRow("ABS12343").Column("Edit").ClickLink();

            ExpectHeader("Commodity Details");
            Set("Country of origin").To("");
            ClickHeader("Commodity Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect("GR - Greece");
            System.Threading.Thread.Sleep(1000);
            Click("GR - Greece");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Click("Save");
            ExpectHeader(That.Contains, "Commodities");
            ClickButton("Complete");
            ExpectHeader(That.Contains, "Consignments");
            ClickLink("Shipments Into UK");
            ExpectHeader("Shipments Into UK");

            //assert edit button invisible
            //Near("Type").ClickLabel("All");
            //Near("Progress").ClickLabel("All");
            Near("Status").ClickLabel("All");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow("R0119000001");
            AtRow("R0119000001").Column("Edit").ExpectNoLink();
        }
    }
}
