using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotUseURLToEditSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithResolvesConsignmentAndCommodityMismatch>();
            //go to shipment edit page
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            Click(What.Contains, "Save");

            WaitToSeeHeader(That.Contains, "Consignments - Into UK");
            AtRow(That.Contains, "R011900000101").Column("Edit").Click("Edit");
            Click(What.Contains, "Save");

            AtRow(That.Contains, "ABS12343").Column("Edit").Click("Edit");
            Set("Gross weight").To("6.66");
            Set("Net weight").To("4.89");
            Set("Number of packages for this commodity code (if known)").To("6");
            Click(What.Contains, "Save");
            Click("Complete");
            Click("Shipment");


            //try to access shipment edit page via url
            GotoCopiedUrl();
            ExpectNoHeader(That.Contains, "Shipment details");
        }
    }
}
