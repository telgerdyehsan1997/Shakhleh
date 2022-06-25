using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class OrderingCompanyUsersByNameforWWL : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyUserForWWLJenny, AddCompanyUserForWWLRichardSmith>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Column("Company name").ClickLink("");
            Expect("Company Users");
            Click("Company Users");
            WaitToSeeHeader("Company Users");

            // ----------------------------------------------
            //AtColumn("First name").

        }
    }
}