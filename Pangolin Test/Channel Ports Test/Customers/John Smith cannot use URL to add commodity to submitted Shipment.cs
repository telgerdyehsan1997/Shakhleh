using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCannotUseURLToAddCommodityToSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to commodity details page
            LoginAs<JohnSmithCustomer>();
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            ExpectRow("R011900000101");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            Click("New Commodity");
            CopyUrl();

            //submit shipment
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments");
            AtRow(That.Contains, "R0119000001").Column("Submit").ClickButton();
            WaitToSeeHeader("Shipments");

            //try to access add commodity page via url
            GotoCopiedUrl();
            ExpectNoHeader("Commodity Details");
        }
    }
}
