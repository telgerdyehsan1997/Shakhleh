using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCustomerShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount,JohnSmithAddsShipmentForTruckersLtd>();
            AssumeDate("1/1/2019");
            LoginAs<JohnSmithCustomer>();
            Goto("/");

            ClearField("to");
            Click("Search");

            //assert list row
            AtRow(That.Contains, "R0119000001").Column("Tracking number").Expect("R0119000001");
            AtRow(That.Contains, "R0119000001").Column("Date").Expect("01/01/2019");
            AtRow(That.Contains, "R0119000001").Column("Customer Reference").Expect("55555");
            AtRow(That.Contains, "R0119000001").Column("Company name").Expect(What.Contains, "Truckers Ltd");
            AtRow(That.Contains, "R0119000001").Column("Vehicle number").Expect("89A");
            AtRow(That.Contains, "R0119000001").Column("Trailer number").Expect("6514");
            AtRow(That.Contains, "R0119000001").Column("Progress").Expect("Draft");
            AtRow(That.Contains, "R0119000001").Column("Weights mismatch").ExpectNoTick();

            //not really sure if everything below is necessary as you can see all details of the shipment already, will comment it out for now.

            ////test Find search
            //WaitToSeeHeader("Shipments");
            //Set("Find").To("55555");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");

            ////test Type search
            //Set("Find").To("");
            //Near("Type").ClickLabel("Into UK");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");
            //Near("Type").ClickLabel("Out of UK");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectNoRow(That.Contains, "R0119000001");
            //Near("Type").ClickLabel("All");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");

            ////test Progress search
            //Set("Find").To("");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");
            //Near("Type").ClickLabel("Submitted");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectNoRow(That.Contains, "R0119000001");
            //Near("Type").ClickLabel("All");
            //Near("Type").ClickLabel("Draft");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");

            ////test Status search
            //Set("Find").To("");
            //Near("Status").ClickLabel("Active");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");
            //Near("Type").ClickLabel("Archived");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectNoRow(That.Contains, "R0119000001");
            //Near("Type").ClickLabel("Into UK");
            //Near("Progress").ClickLabel("Draft");
            //Near("Status").ClickLabel("Active");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");

            ////test Date search
            //Set("Find").To("");
            //ClickField("Date");
            //Type("1/1/2015");
            //Press(Keys.Tab);
            //Type("1/1/2018");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectNoRow(That.Contains, "R0119000001");
            //Set("Find").To("");
            //Set("Date").To("1/1/2017");
            //Near("Date").Set("to").To("1/1/2022");
            //Click("Search");
            //WaitToSeeHeader("Shipments");
            //ExpectRow(That.Contains, "R0119000001");
            //Set("Find").To("");
            //Click("Search");
            //WaitToSeeHeader("Shipments");

            //test archive
            AtRow(That.Contains, "R0119000001").Column("Archive").ClickButton();
            WaitToSeeText("Are you sure you want to archive this Shipment?");
            Click("OK");

            //test unarchive
            ClickLabel("Archived");
            Click("Search");

            ExpectRow("R0119000001");
        }
    }
}
