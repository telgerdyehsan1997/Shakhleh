using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TrackingNumberSuffixesShouldOnlyResetOnceAtStartOfEachMonth : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAnotherShipmentForTruckers1MonthLater>();

            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/02/2020");
            AssumeTime("12:00");
            Goto("/");

            CheckBackgroundTasks();
            AtRow("Reset shipment tracking number").ClickLink();
            Goto("/");

            Click(The.Top, "Shipments");
            WaitToSeeHeader(That.Contains, "Shipments");

            Click("New Shipment");
            ClickHeader("Shipment Details");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALAN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALAN SMITH");
            ClickLabel("Group");
            Click(What.Contains, "---Select---");
            WaitToSee("Import");
            Click("Import");
            Set("Customer Reference").To("RT1234567");
            ClickLabel("Into UK");
            Set("Vehicle number").To("378");
            Set("Trailer number").To("3587");
            ClickField("Route");
            Expect(What.Contains, "CALAIS to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Portsmouth");

            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("03/02/2020");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            ClickLink("Shipments");
            ExpectHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");


            ExpectRow("R0220000001");
            AtRow("R0220000001").Column("Type").Expect("Into UK");
        }
    }
}