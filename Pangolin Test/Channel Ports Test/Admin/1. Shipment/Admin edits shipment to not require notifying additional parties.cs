using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminEditsShipmentToNotRequireNotifyingAdditionalParties : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewShipmentForTruckersLtd>();

            LoginAs<ChannelPortsAdmin>();

            AssumeDate("01/01/2020");
            Goto("/");

            Click(The.Top, "Shipments");
            WaitToSeeHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0721000001");
            AtRow("R0721000001").Click("Edit");

            WaitToSeeHeader("Shipment Details");

            ClickLabel("Not required");

            Click(What.Contains, "Save and Add");

            ExpectNo("The Group field is required.");
            ExpectNo("The Contact name field is required.");

            Click(The.Top, "Shipments");
            WaitToSeeHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0721000001");

            Click("R0721000001");

            WaitToSee(What.Contains, "Shipment Details");

            AtLabel("Notify additional parties").Expect("Not required");

            ExpectNoLabel("Group");
            ExpectNoLabel("Contact name");
        }
    }
}