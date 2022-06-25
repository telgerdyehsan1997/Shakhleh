using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCannotUseURLToAccessConsignmentsList : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to shipment edit page
            LoginAs<JohnSmithCustomer>();
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment details");
            Click(What.Contains, "Save");
            WaitToSeeHeader(That.Contains, "Consignments");
            CopyUrl();
            /*AtRow("R011900000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");
            Click(What.Contains, "Save");
            Click("Complete"); */

            //submit shipment
            LoginAs<JohnSmithCustomer>();
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            //AtRow(That.Contains, "R0119000001").Column("Submit").ClickButton();

            //try to access add consignment page via url
            GotoCopiedUrl();
            //ExpectNoHeader(That.Contains, "Consignments");
        }
    }
}
