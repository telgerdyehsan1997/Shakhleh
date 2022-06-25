using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewTaxLine_A00 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");

            ExpectHeader("Users");

            //Navigates to Tax Lines
            ClickLink("Default Tax Lines");

            ExpectHeader("Default Tax Lines");

            //Adds the new Tax Line
            ClickLink("New Default Tax Line");
            ExpectHeader("Default Tax Line Details");
            ClickLabel("A");
            Set("Tax Type Suffix").To("00");
            ClickButton("Save");

            //Asserts that the tax line has been saved
            ExpectRow("A00");
        }
    }
}