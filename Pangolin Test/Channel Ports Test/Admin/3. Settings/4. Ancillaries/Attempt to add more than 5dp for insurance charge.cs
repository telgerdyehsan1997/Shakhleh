using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AttemptToAddMoreThan5dpForInsuranceCharge : UITest
    {
        [TestProperty("Sprint", "1")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<SetAncillariesForUK>();

            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            Click("Ancillaries");
            AtRow("GB").Click("Edit");

            //Insurance charge has not been implemented
            //Set("Insurance charge").To("10.123456");
            Click("Save");

            Expect("The Insurance charge can only be set to a maximum of five decimal places.");
        }
    }
}