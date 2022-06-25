using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCPCsToOmega : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddCompanyOmega,CreateNewCPC_12345,CreateNewCPC_54321>();
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            AtRow("Omega").ClickLink("Omega");
            ClickLink("Special CPCs");
            ExpectHeader("Special CPCs");

            Click("New Special CPC");
            ExpectHeader("Special CPC Details");

            AtLabel("CPC").Type("12345");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Click("Save");



        }
    }
}
