using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Customer_CompleteEADConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount, AddCommodityToAConsignment>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow("Truckers Ltd").Click("Edit");
            ClickLabel("Not NCTS");
            Click("Save");

            LoginAs<JohnSmithCustomer>();

            AssumeDate("01/01/2020");
            Goto("/");
            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Set("Date").To("19/05/2018");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            ExpectRow("R0719000001");





            //AtRow("R012000000101").Column("Progress").Expect("Draft");
            //AtRow("R012000000101").Column("Transmit").ExpectNo("Transmit");

            //Goto("/");
            //Click(The.Top, "Shipments Into UK");
            //WaitToSeeHeader("Shipments Into UK");
            //Set("Date created").To("01/01/1999");
            //Set("Expected date of arrival/departure").To("01/01/1999");
            // Set(The.Top, "to").To("25/12/2025");
            //Set(The.Bottom, "to").To("25/12/2025");
            // ClickButton("Search");

            //AtRow("R0120000001").Column("Consignments").ClickLink();

            //WaitToSeeHeader(That.Contains, "Consignments - Into UK");
            //AtRow(That.Contains, "R0120000001").Column("Progress").Expect("Draft");
            //AtRow(That.Contains, "R0120000001").Column("Edit").Expect("Edit");

            //AtRow(That.Contains, "R0120000001").Column("Commodities").ClickLink();

            //WaitToSeeHeader(That.Contains, "R012000000101 - Commodities");

            //ExpectButton("Complete");

            //// Validate incorrect (currency) values
            //ClickButton("Complete");

            //Expect(What.Contains, "The total value for consignment is £300.00 and the total value for the commodities within this consignment is £1000.00. These values do not match.");

            //Click("OK");

            //AtRow("ABS12343").Click("Edit");

            //ExpectHeader(That.Contains, "Commodity Details");
            //Set("Value").To("300");
            //Click("Save");

            //AtRow("ABS12343").Column("Value").Expect("300");
            //Expect(What.Contains, "Total: 300");

            //Click("Complete");

            //WaitToSeeHeader(That.Contains, "Consignments - Into UK");
            //AtRow(That.Contains, "R0120000001").Column("Progress").Expect("Ready to transmit");
            //AtRow(That.Contains, "R0120000001").Column("Edit").ExpectNo("Edit");

            //Goto("/");
            //Click(The.Top, "Shipments Into UK");
            //WaitToSeeHeader("Shipments Into UK");
            //Set("to").To("");
            //Click("Search");

            //AtRow("R0120000001").Column("Progress").Expect("In Progress");
            //AtRow("R0120000001").Column("Edit").ExpectNo("Edit");
            //AtRow("R0120000001").Column("Tracking number").ClickLink();

            //ExpectHeader(That.Contains, "Shipment Details");

            //ExpectRow("R012000000101");
            //AtRow("R012000000101").Column("Progress").Expect("Ready to transmit");
            //AtRow("R012000000101").Column("Transmit").Expect("Transmit");
        }
    }
}
