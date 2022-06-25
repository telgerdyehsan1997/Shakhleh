using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCannotUseURLToAddConsignmentToSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to shipment edit page
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Set("Date created").To("28/06/2018");
            Set("Expected date of arrival/departure").To("28/06/2018");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            Click("New Consignment");
            CopyUrl();

            //submit shipment
            ClickLink("Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");

            /*AtRow(That.Contains, "R0119000001").Column("Tracking number").ClickLink();
            ExpectHeader("Shipment Details");
            AtRow("R011900000101").Column("Progress").Click("Draft");

            //try to access add consignment page via url
            GotoCopiedUrl();
            ExpectNoHeader(That.Contains, "Consignment Details"); */
        }
    }
}
