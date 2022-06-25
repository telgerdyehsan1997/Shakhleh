using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCannotUseURLToEditConsignmentInSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to consignment edit page
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Set("Date").To("");
            ClickField("Date");
            Type("01/01/1900");
            Press(Keys.Tab);
            Type("01/01/2100");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Edit").Click("Edit");
            CopyUrl();

            //submit shipment
            LoginAs<JohnSmithCustomer>();
            ExpectHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").ClickLink();
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            ExpectRow("R011900000101");
            AtRow("R011900000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");
            Click("Save and Add Commodities");
            Click("Complete");
            ClickLink("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("R0119000001").Column("Tracking number").ClickLink();
            ExpectHeader("Shipment Details");

            //try to access edit consignment page via url
            GotoCopiedUrl();
            ExpectNoHeader("Consignment Details");
        }
    }
}
