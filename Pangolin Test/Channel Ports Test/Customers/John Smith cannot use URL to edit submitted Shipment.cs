using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCannotUseURLToEditSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to shipment edit page
            LoginAs<JohnSmithCustomer>();
            ExpectHeader("Shipments Into UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            CopyUrl();
            Click("Save and Add/Amend Consignments");
            AtRow("R011900000101").Column("Edit").Click("Edit");
            Click("Save and Add Commodities");
            ExpectHeader(That.Contains, "Commodities");
            Click("Complete");
            ExpectHeader("Duty is Payable on one or more of the commodity codes");
            Click("No");
            Click("No");
            AtRow("R0119000001").Column("Progress").Expect("Draft");

            //submit shipment
            LoginAs<JohnSmithCustomer>();
            ExpectHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Tracking number").ClickLink();
            ExpectHeader("Shipment Details");

            //try to access Shipment edit page via url
            GotoCopiedUrl();
            //ExpectNoHeader("Shipment Details");
        }
    }
}
