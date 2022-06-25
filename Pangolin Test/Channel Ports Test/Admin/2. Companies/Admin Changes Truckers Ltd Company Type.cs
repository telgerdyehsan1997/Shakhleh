using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminChangesTruckersLtdCompanyType : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            AssumeDate("1/1/2019");
            Goto("/");
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Edit").Click("Edit");
            WaitToSeeHeader("Record Details");
            ClickLabel("Customer");
            Set("Customer account number").To("A3333");
            Set("Guarantee type").To("R");
            Click("Save");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Type").Expect("Customer");
        }
    }
}