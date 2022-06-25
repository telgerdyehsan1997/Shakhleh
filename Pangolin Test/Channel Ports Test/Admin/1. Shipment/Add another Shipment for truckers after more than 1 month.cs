using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnotherShipmentForTruckersAfterMoreThan1Month : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAnotherShipmentForTruckers1MonthLater>();

            AssumeDate("02/02/2020");
            Goto("/");

            Click(The.Top, "Shipments");
            WaitToSeeHeader(That.Contains, "Shipments");
            Click("New Shipment");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            Set("Primary contact").To("");
            ClickHeader("Shipment Details");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "JOHN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "JOHN SMITH");

            AtLabel("Notify additional parties").ClickLabel("Group");
            Set(The.Bottom, "Group").To("IMPORT");
            Set("Customer Reference").To("RT564744");
            ClickLabel("Into UK");
            Set("Vehicle number").To("3jkdhf7");
            Set("Trailer number").To("3587");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CALAIS to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Portsmouth");

            Click("Save and Add/Amend Consignments");
            System.Threading.Thread.Sleep(1000);
            Type("01/02/2020");
            System.Threading.Thread.Sleep(1000);
            Click("Save and Add/Amend Consignments");

            Click("Shipments");
            WaitToSeeHeader("Shipments");
            ClickLabel(The.Top, "All");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0220000003");
            AtRow("R0220000003").Column("Type").Expect("Into UK");
        }
    }
}