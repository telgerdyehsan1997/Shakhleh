using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnotherShipmentForTruckersWithin1Month : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewShipmentForTruckersLtd, AddContactsToImport>();
            LoginAs<ChannelPortsAdmin>();

            WaitToSeeHeader("Shipments");
            Click("New Shipment");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickLabel("Into UK");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CALAIS to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Portsmouth");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Alan Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Alan Smith");

            /*ClickLabel("Group");
            Click(What.Contains, "---Select---");
            WaitToSee("Import");
            Click("Import"); */
            Set("Customer Reference").To("RT564746");
            Set("Vehicle number").To("387");
            Set("Trailer number").To("358");

            /*Set("Port of arrival").To("Blackpool");
            Click(What.Contains, "Blackpool"); */
            Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            Type("31/01/2022");
            System.Threading.Thread.Sleep(1000);
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignment Details");
            Click("Shipments");
            WaitToSeeHeader("Shipments");
            ClickLabel(The.Top, "All");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");

            Click("Search");

            ExpectRow("358");
            AtRow("358").Column("Type").Expect("Into UK");
        }
    }
}
