using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddWarehouse1AsTruckersAuthorisedLocation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCountry_France,AdminAddsCompanyTruckersLtd,AddAuthorisedLocationWarehouse1>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            AtRow(That.Contains, "Truckers").Click("Edit");
            Click("Save");

        }
    }
}
