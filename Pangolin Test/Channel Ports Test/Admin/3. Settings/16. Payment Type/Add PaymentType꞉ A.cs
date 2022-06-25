using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class PaymentTypeA : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "128144")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("Payment Types");

            WaitToSeeHeader("Payment Types");

            Click("Payment Type");

            WaitToSeeHeader("Payment Type Details");
            Set("Code").To("A");
            Set("Description").To("code A");

            Click("Save");

            WaitToSeeHeader("Payment Types");
            ExpectRow("A");
            AtRow("A").Column("Code").Expect("A");
        }
    }
}