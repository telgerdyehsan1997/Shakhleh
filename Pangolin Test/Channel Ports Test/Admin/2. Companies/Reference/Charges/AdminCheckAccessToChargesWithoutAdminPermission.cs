using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCheckAccessToChargesWithoutAdminPermission : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminCheckAccessToCharges>();

            LoginAs<AliceSpatAdmin>();

            ExpectHeader("Shipments");

            Click("Companies");
            AtRow("Channel Ports").Column("Company name").Click("Channel Ports");
            ExpectNoLink("Charges");


            
        }
    }
}