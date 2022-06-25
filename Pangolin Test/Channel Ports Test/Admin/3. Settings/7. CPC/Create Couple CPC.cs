using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateCoupleCPC : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCPC_12345>();
            Run<CreateNewCPC_54321>();

            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("CPC");

            ExpectHeader("CPC");

        }
    }
}