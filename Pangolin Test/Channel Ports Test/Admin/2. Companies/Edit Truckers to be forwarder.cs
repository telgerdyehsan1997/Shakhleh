using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditTruckersToBeForwarder : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //create new company
            AtRow("TRUCKERS LTD").Click("Edit");

            ExpectHeader("Record Details");
            AtLabel("GVMS").ClickLabel("Not GVMS");
            ClickLabel("Forwarder");
            AtLabel("CFSP").ClickLabel("None");
            Set("Customer account number").To("A4642");

            Click("Save");

            ExpectHeader("Companies");
            AtRow("TRUCKERS LTD").Column("Type").Expect("Forwarder");
            AtRow("TRUCKERS LTD").Column("Customer account number").Expect("A4642");
        }
    }
}