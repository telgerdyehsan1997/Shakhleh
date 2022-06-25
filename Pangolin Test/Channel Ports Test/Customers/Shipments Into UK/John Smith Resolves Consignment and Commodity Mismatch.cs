using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithResolvesConsignmentAndCommodityMismatch : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsCommodityToConsignment>();

            //navigate
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Set("Date created").To("28/06/2018");
            Set("Expected date of arrival/departure").To("28/06/2018");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("1");
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            Click("Complete");

            /* Expect("The total value for consignment is £1200.00 and the total value for the commodities within this consignment is £749.99. These values do not match.");
             ClickButton("Ok"); */

            //resolve value mismatch
            Goto("/");
            ExpectHeader("Shipments Into UK");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow("R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            //ExpectHeader("Consignments - Into UK");
            AtRow("R011900000101").Column("Edit").Click("Edit");
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
            ClickButton("Complete");
        }
    }
}