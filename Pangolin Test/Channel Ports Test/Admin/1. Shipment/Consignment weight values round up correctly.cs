using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ConsignmentWeightValuesRoundUpCorrectly : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentToTruckersLtd>();

            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0721000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");

            Click("Save and Add/Amend Consignments");

            WaitToSeeHeader(That.Contains, "Consignments - Into UK");
            AtRow(That.Contains, "R072100000101").Column("Edit").Click("Edit");
            WaitToSeeHeader("Consignment Details");
            Set("Total gross weight").To("3.33");
            Set("Total net weight").To("2.22");
            Click("Save and Add Commodities");

            WaitToSeeHeader(That.Contains, "Commodities");
            //AtRow(That.Contains, "R012000000101").Column("Total gross weight").Expect("3.34 kg");
            //AtRow(That.Contains, "R012000000101").Column("Total net weight").Expect("2.223 kg");

            NearLabel("Consignment total gross weight").Expect("3.33 kg");
            NearLabel("Consignment total net weight").Expect("2.22 kg");
            NearLabel("Consignment total packages").Expect("10");
        }
    }
}