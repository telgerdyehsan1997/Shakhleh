using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GlobalSettings_AssertMandatoryValidation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<Undischarged_ChannelPortsAdmin>();

            //Navigates to Global Settings
            ClickLink("Settings");
            Click("Global Settings");
            ExpectHeader("Global Settings");

            //Clicks Save and then asserts the mandatory field validation
            ClickButton("Save");
            Expect("The Days before reminder email is sent field is required.");
            Expect("The Maximum number of reminder Emails field is required.");
            Expect("The Delay between creating the record and sending stage 1 email field is required.");
        }
    }
}