using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditDefaultLicenseBasic : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddDefaultLicenseBasic>();
            LoginAs<ChannelPortsAdmin>();

            Click("Accounting");
            Click("Default Licenses");
            ExpectHeader("Default Licenses");
            ExpectRow("Basic");

            AtRow("Basic").Click("Edit");
            ExpectHeader("Default License Details");
            AtField("Name").Expect("Basic");
            Set("License Fee").To("100");
            Click("Save");
            ExpectHeader("Default Licenses");
            AtRow("Basic").Column("License Fee").ExpectNo("50");
            AtRow("Basic").Column("License Fee").Expect("100.00");
        }
    }
}