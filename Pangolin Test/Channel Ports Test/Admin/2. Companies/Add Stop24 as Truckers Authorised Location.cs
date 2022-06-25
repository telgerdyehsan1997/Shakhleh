using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddStop24AsTruckersAuthorisedLocation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow(That.Contains, "Truckers").Click("Edit");
            Set("Authorised locations").To("Stop 24");
            Click("Save");

            ExpectHeader("Companies");
        }
    }
}