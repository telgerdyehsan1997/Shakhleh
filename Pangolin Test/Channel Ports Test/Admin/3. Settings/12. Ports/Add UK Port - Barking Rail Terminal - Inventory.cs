using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddUKPort_BarkingRailTerminal_Inventory : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Ports
            ClickLink("Settings");

            ClickLink("Ports");

            ExpectHeader("Ports");
            ClickLink("New Port");

            //Adds the Port Details
            ExpectHeader("Port Details");
            Set("Port name").To("BARKING RAIL TERMINAL");
            Set("Transport mode").To("6");
            Set("Country").To("United Kingdom");
            AtLabel("Non-UK").ClickLabel("No");
            Set("Port code").To("LBK");
            this.ClickAndWait("NCTS code", "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom - GB000060");
            AtLabel("Into UK Type").ClickLabel("Inventory");
            Set("Into UK Value").To("D");
            AtLabel("Out Of UK Type").ClickLabel("Inventory");
            Set("Out of UK Value").To("D");
            Set("DTI Badge").To("KLV");
            ClickButton("Save");

            //Assert that the port has been saved
            ExpectRow("BARKING RAIL TERMINAL");
        }
    }
}