using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddMultipleCompaniesAndCheckOrder : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddCompanyAlpha,AddCompanyDelta,AddCompanyOmega>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Companies");
            ExpectHeader("Companies");

            ClickLink("Shipments");

            ClickLink("New Shipment");

            ExpectHeader("Shipment Details");

        }
    }
}
