using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertConsignmentSearchFilterActiveTextVisible : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var consignmentNumber = "R072100000101";

            Run<AddShipmentIntoUKASMAccepted>();

            LoginAs<ChannelPortsAdmin>();

            //Uses the Consignment search
            Click("Consignment Search");
            ExpectHeader("Consignment- Into/Out of UK");
            Set("Consignment number").To(consignmentNumber);
            ClickButton(The.Left, "Search");

            //Asserts that Active Consignment Search text present
            Expect(What.Contains, "Consignment Level Search Filter: Active");
        }
    }
}