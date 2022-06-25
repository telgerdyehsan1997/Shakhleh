using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompanyUserLogsInToViewBroadcastedMessages : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<BroadcastMessageToCompanies>();
            LoginAs<Goro_MajimaConstruction>();

            //Asserts that 'Broadcasts' page opens by default as there is a new message
            ExpectHeader("Broadcasts");

            //Asserts that the message has been received
            ExpectRow("Broadcasted Subject");
        }
    }
}