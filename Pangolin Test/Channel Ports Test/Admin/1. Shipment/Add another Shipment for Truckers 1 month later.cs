using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnotherShipmentForTruckers1MonthLater : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAnotherShipmentForTruckersWithin1Month>();

            ExpectHeader("Shipments");
            Click("New Shipment");

            AtField("Company name").Type("Truck");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);


            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Click("Alan Smith");
            ClickLabel("Group");
            Click(What.Contains, "---Select---");
            WaitToSee("Import");
            Click("Import");
            Set("Customer Reference").To("RT564745");

            Set("Vehicle number").To("37QA");
            Set("Trailer number").To("3587");
            Set("Expected date of departure").To("01/02/2022");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Portsmouth to CALAIS");
            AtLabel("NCTS").ClickLabel("No");

            Click("Save and Add/Amend Consignments");

            WaitToSeeHeader("Consignment Details");

            Click("Shipments");
            WaitToSeeHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");

            Click("Search");

            ExpectRow("R0721000001");

        }
    }
}