using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddUK_PortGVMSAndInventory : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Ports";
            var intoUkType = new string[] { IntoUKType.GVMS, IntoUKType.Inventory };
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipments
            this.NavigateToSettingsPage(settingsPage);

            //Creates the Port
            this.AddPort(
                  portName: "Both",
                  portCountry: "United Kingdom",
                  nonUk: true,
                  portCode: "BOT",
                  nctsCode: "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom - GB000060",
                  intoUkType: intoUkType,
                  intoUkValue: "A",
                  outOfUkType: OutOfUKType.GVMS,
                  outOfUkValue: "A"
            );
        }
    }
}