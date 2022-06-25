using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Commodities_AssertHealthCertificateFields : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentToSafetyAndSecurityShipment>();
            LoginAs<ChannelPortsAdmin>();


        }
    }
}