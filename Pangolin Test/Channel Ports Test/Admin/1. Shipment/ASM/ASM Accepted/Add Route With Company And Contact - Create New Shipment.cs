using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddRouteWithCompanyAndContact_CreateNewShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRoutePortsmouthToAmsterdam, AdminAddsCompanyGeeksQA, AddProductForGeeksQA, AddNewContactForGeeksQARafalQA, CreateNewTransitOfficePL, AddCompanyAPISettingsRaf>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("New Shipment"); //error will occur after clicking New Shipment
        }
    }
}