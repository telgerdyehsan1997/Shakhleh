using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnotherShipmentForWWLAfterMoreThan1Months : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAnotherShipmentForWWL1MonthsLater>();

            LoginAs<ChannelPortsAdmin>();
            AssumeDate("02/02/2020");
            Goto("/");

            Click(The.Top, "Shipments");
            WaitToSeeHeader("Shipments");
            Click("New Shipment");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            Set("Primary contact").To("");
            ClickHeader("Shipment Details");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Jenny Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Jenny Smith");
            ClickHeader("Shipment details");
            ClickLabel("Out of UK");
            Set("Customer Reference").To("CusRef0101");
            Set("Vehicle number").To("896");
            Set("Expected date of departure").To("02/02/2020");
            //set port of departure
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Portsmouth to CALAIS");
            Click("Save and Add/Amend Consignments");

            Click("Shipments");
            WaitToSeeHeader(That.Contains, "Shipments");
            NearLabel("Out of UK").ClickLabel("All");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("02/04/2025");
            Set(The.Bottom, "to").To("02/04/2025");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Shipment");
            AtRow("T0220000001").Column("Company name").Expect("Worldwide Logistics Ltd");
            AtRow("T0220000001").Column("Tracking number").Expect("T0220000001");
            AtRow("T0220000001").Column("Type").Expect("Out of UK");
            AtRow("T0220000001").Column("Date").Expect("02/02/2020");
            AtRow("T0220000001").Column("Vehicle number").Expect("896");
        }
    }
}