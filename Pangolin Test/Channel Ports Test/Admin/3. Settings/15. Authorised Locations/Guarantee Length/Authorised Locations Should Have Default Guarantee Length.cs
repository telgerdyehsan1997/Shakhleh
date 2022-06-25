using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AuthorisedLocationsShouldHaveDefaultGuaranteeLength : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to settings
            ClickLink("Settings");
            ExpectHeader("Users");

            //Navigates to Authorised Locations
            ClickLink("Authorised Locations");
            ExpectHeader("Authorised Locations");

            //Asserts that Guarantee length has a default value
            AtRow("Stop 24").Column("Guarantee length").Expect("1");
            AtRow("Stop 24").Column("Guarantee length").ClickLink("1");

            //Asserts that this default value is 8 days
            ExpectHeader("Guarantee Length");
            AtRow("8").Column("Valid for (days)").Expect("8");
            //AtRow("8").Column("Actions").ExpectNo("Edit");
            AtRow("8").Column("Archive").ExpectNo("Archive");

        }
    }
}