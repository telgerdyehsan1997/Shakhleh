using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotUseURLToEditConsignmentInSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CompleteConsignmentsForTruckersLtd>();

            //go to consignment edit page
            LoginAs<ChannelPortsAdmin>();

            AssumeDate("04/10/2022");
            Goto("/");
            WaitToSeeHeader("Shipments");

            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            //Sets the progress to 'Draft' so that the Consignment page can be accessed
            AtRow(That.Contains, "R0721000001").Column("Tracking number").ClickLink();
            WaitToSeeHeader("Shipment details");
            AtRow("R072100000101").Column("Progress").Click("Ready to Transmit");
            ClickButton("Save");
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            AtRow("R0721000001").Column("Edit").Click("Edit");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R072100000101").Column("Edit").Click("Edit");
            CopyUrl();

            //Completes the Shipment so it can be 'Ready to Transmit' again
            Click("Save and Add Commodities");
            ClickButton("Complete");

            //submit shipment
            LoginAs<ChannelPortsAdmin>();

            AssumeDate("04/10/2022");
            Goto("/");


            WaitToSeeHeader("Shipments");

            //try to access edit consignment page via url
            GotoCopiedUrl();
            ExpectNoHeader("Consignment Details");
        }
    }
}