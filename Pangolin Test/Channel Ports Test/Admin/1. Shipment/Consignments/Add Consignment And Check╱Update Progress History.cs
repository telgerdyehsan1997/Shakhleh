using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddConsignmentAndCheckUpdateProgressHistory : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentIntoUKASMAccepted>();

            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            Set("Date created").To("19/01/2022");
            Set(The.Top, "to").To("25/12/2025");
            Set("Expected date of arrival/departure").To("19/01/2022");
            Set(The.Bottom, "to").To("25/12/2025");

            Click("Search");

            ExpectRow("R0721000001");

            AtRow("R0721000001").Column("Tracking number").ClickLink();
            ExpectHeader("Consignments - Into UK");

            //----change Progress status
            AtRow("R072100000101").Column("Progress").ClickLink();
            ExpectHeader("Progress History");

            Set("Progress").To("Partial");
            Click("Save");

            AtRow("R072100000101").Column("Progress").Expect("Partial");


        }
    }
}