using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithEditsExistingShipment : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JennySmithAddsShipmentForWWL_IntoUK>();

            LoginAs<JennySmithCustomer>();

            AssumeDate("01/02/2020");
            Goto("/");

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0220000001");
            AtRow("R0220000001").Click("Edit");
            WaitToSeeHeader("Shipment Details");

            Expect("Type");
            //AtLabel("Type").Expect("Into UK");
            //AtLabel("Type").ExpectNo("Out of UK");

            Set("Customer Reference").To("09876");

            Set("Vehicle number").To("12345");

            Click(The.Top, "Shipments Into Uk");
            WaitToSeeHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("R0220000001").Column("Customer Reference").ExpectNo("09876");
            AtRow("R0220000001").Column("Customer Reference").Expect("12345");
            AtRow("R0220000001").Column("Vehicle number").ExpectNo("12345");
            AtRow("R0220000001").Column("Vehicle number").Expect("43210");
            AtRow("R0220000001").Column("Trailer number").Expect("56789");

            AtRow("R0220000001").Click("Edit");
            WaitToSeeHeader("Shipment Details");

            Set("Customer Reference").To("09876");
            Set("Vehicle number").To("12345");
            Set("Trailer number").To("");

            Click("Save and Add/Amend Consignments");

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("R0220000001").Column("Customer Reference").ExpectNo("12345");
            AtRow("R0220000001").Column("Customer Reference").Expect("09876");
            AtRow("R0220000001").Column("Vehicle number").ExpectNo("43210");
            AtRow("R0220000001").Column("Vehicle number").Expect("12345");
            AtRow("R0220000001").Column("Trailer number").ExpectNo("56789");
        }
    }
}