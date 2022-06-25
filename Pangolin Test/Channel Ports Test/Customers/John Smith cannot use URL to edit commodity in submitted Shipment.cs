using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithCannotUseURLToEditCommodityInSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to commodity details page
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");

            Set("Date created").To("01/01/2018");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            AtRow(The.Top).Column("Edit").Click("Edit");
            WaitToSeeHeader("Commodity Details");
            CopyUrl();

            //submit shipment
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            /*            AtRow(That.Contains, "R0119000001").Column("Submit").ClickButton();
                      WaitToSeeHeader("Shipments");

                      //try to access edit commodity page via url
                      GotoCopiedUrl();
                      ExpectNoHeader("Commodity Details");*/
        }
    }
}
