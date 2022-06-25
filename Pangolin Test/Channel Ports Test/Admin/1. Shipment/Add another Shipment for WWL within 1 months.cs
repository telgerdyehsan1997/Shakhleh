using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSharp.Framework;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnotherShipmentForWWLWithin1Month : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentForWWL>();

            LoginAs<ChannelPortsAdmin>();
            AssumeDate("31/01/2020");
            Goto("/");

            Click(The.Top, "Shipments");
            WaitToSeeHeader("Shipments");
            Click("New Shipment");

            ClickField("Company name");
            Type("World");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Worldwide Logistics Ltd");
            Set("Primary Contact").To("");
            ClickHeader("Shipment Details");
            ClickField("Primary contact");
            //Type("Jenny");
            System.Threading.Thread.Sleep(1000);
            Click("Jenny Smith");
            ClickLabel("Specific contacts");
            Click(What.Contains, "Nothing selected");
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            ClickHeader();
            ClickLabel("Out of UK");
            Set("Customer Reference").To("CustomerRef1");
            Set("Vehicle number").To("892");
            Set("Expected date of departure").To("31/01/2020");
            Click(What.Contains, "Save and Add/Amend Consignments");

            Click("Shipments");
            WaitToSeeHeader("Shipments");
            ClickLabel(The.Top, "All");
            Set("Date created").To("01/01/2019");
            Set("Expected date of arrival/departure").To("01/01/2019");
            Set(The.Top, "to").To("01/02/2029");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            ExpectRow("T0721000001");
            AtRow("T0721000001").Column("Tracking number").Expect("T0721000001");
            AtRow("T0721000001").Column("Type").Expect("Out of UK");
            AtRow("T0721000001").Column("Date").Expect("01/07/2021");
        }
    }
}