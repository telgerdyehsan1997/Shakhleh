using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithResolvesWeightMismatch : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsCommodityToConsignment>();

            //navigate
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Set("Date").To("");
            ClickField("Date");
            Type("01/01/1900");
            Press(Keys.Tab);
            Type("01/01/2100");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            Click("Complete");
            Expect("The total value for consignment is £1200.00 and the total value for the commodities within this consignment is £749.99. These values do not match.");
            Click("Ok");

            //resolve value mismatch
            Click("Back");
            AtRow(That.Contains, "R011900000101").Click("Edit");
            WaitToSeeHeader(That.Contains, "Consignment Details");
            Set("Total value").To("749.99");
            Click("Save and Add Commodities");

            //todo check wether this button should be visible here, currently is
            //ExpectNo("Complete");

            //resolve no. of packages mismatch
            AtRow("ABS12343").Click("Edit");
            Set("Number of packages for this commodity code (if known)").To("6");
            ClickHeader("Commodity Details");

            Click("Save");
            Expect("Complete");
        }
    }
}