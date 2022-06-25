using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnotherShipmentForWWL1MonthsLater : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAnotherShipmentForWWLWithin1Month>();

            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/02/2020");
            Goto("/");

            CheckBackgroundTasks();
            AtRow("Reset shipment tracking number").ClickLink();
            Goto("/");

            Click(The.Top, "Shipments");
            WaitToSeeHeader("Shipments");
            Click("New Shipment");

            ClickField("Company name");
            Type("Worldwide Logistics");
            System.Threading.Thread.Sleep(3000);
            Click(What.Contains, "Worldwide");

            Set("Primary Contact").To("");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Jenny Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Jenny Smith");
            //Set("Contact type").To("Specific contact");
            //ClickLabel("Specific contacts");
            ClickHeader("Shipment Details");
            ClickLabel("Out of UK");
            Set("Customer Reference").To("CusRef1");
            Set("Vehicle number").To("892");
            Set("Expected date of departure").To("01/02/2020");
            //set port of departure
            // Click("Blackpool");
            Click("Save and Add/Amend consignments");
            Click("Shipments");

            WaitToSeeHeader(That.Contains, "Shipments");
            NearLabel("Into UK").ClickLabel("All");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Shipments");
            Set("Date created").To("29/01/2019");
            Set("Expected date of arrival/departure").To("29/01/2019");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("T0721000001");
        }
    }
}