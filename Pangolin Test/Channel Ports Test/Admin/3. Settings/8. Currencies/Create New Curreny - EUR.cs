using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCurreny_EUR : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to settings
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Currencies");
            ExpectHeader("Currencies");
            ExpectRow("EUR");
        }
    }
}