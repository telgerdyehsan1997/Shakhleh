using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithViewsEADConsignmentLogsPage_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount, AddConsignmentToTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Click("Edit");
            WaitToSeeHeader(That.Contains, "Record Details");
            ClickLabel("Not NCTS");
            Click("Save");

            LoginAs<JohnSmithCustomer>();
            AssumeDate("01/01/2020");
            Goto("/");

            Click("Shipments Into UK");

            Set("Date").To("01/01/2020");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            WaitToSeeHeader("Shipments Into UK");
            AtRow(That.Contains, "R0719000001").Column("Tracking number").ClickLink();

            WaitToSeeHeader("Shipment Details");

            ExpectRow("R071900000101");
            ExpectColumn("Logs");
            AtRow("R071900000101").Column("Logs").ClickLink();

            ExpectHeader(That.Contains, "Logs");

            ExpectButton("Back");
        }
    }
}
