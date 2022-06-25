using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithAddsCommoditiesWithWeightValuesOnlyPartCompleted : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsCommodityToConsignment, JohnSmithAddsCommodityToConsignmentIPhone>();
            //navigate
            LoginAs<JohnSmithCustomer>();

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink();
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");
            ClickButton("Complete");
            ExpectHeader("Duty is Payable on one or more of the commodity codes");
            Click("No");
            Click("No");
            ExpectHeader("Shipments Into UK");

            //while weights are all completed, check 'Complete' work
            AtRow("R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink();
            WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            //remove weights for iPhone
            AtRow(The.Top, "ABS12345").Column("Edit").Click("Edit");
            WaitToSeeHeader("Commodity Details");
            Set("Gross weight").To("");
            Set("Net weight").To("");
            AtLabel("Currency").Expect("Great Britain - GBP");
            Set("Value").To("129.99");
            Click("Save");

            //while one commodity has weights missing; check 'Complete' throws validation
            Expect(What.Contains, "The Gross weight field is required.");
            Expect(What.Contains, "The Net weight field is required.");
            Click("Cancel");

            //remove weights for other item - iPad
            AtRow(The.Bottom, "ABS12343").Column("Edit").Click("Edit");
            WaitToSeeHeader("Commodity Details");
            Set("Gross weight").To("");
            Set("Net weight").To("");
            Click("Save");
            Expect(What.Contains, "The Gross weight field is required.");
            Click("Cancel");

            //check 'Complete' works
            Click("Complete");
            ExpectHeader("Duty is Payable on one or more of the commodity codes");
        }
    }
}
