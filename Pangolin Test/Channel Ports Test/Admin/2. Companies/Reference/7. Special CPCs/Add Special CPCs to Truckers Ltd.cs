using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddSpecialCPCsToTruckersLtd : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCPC_12345, CreateNewCPC_54321, AdminAddsCompanyTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            AtRow("Truckers LTD").ClickLink("Truckers LTD");
            ClickLink("Special CPCs");
            ExpectHeader("Special CPCs");

            Click("New Special CPC");
            ExpectHeader("Special CPC Details");

            ClickField("CPC");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CP54321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CP54321");
            Click("Save");

            AtRow("CP54321").Column("CPC Number").Expect("CP54321");
            AtRow("CP54321").Column("CPC description").Expect("THIS IS CPC_54321");
        }
    }
}
