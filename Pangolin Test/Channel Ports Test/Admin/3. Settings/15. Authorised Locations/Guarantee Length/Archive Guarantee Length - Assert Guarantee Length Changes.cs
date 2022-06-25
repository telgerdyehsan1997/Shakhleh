using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveGuaranteeLength_AssertGuaranteeLengthChanges : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Authorised Locations
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Authorised Locations");
            ExpectHeader("Authorised Locations");
            AtRow("Stop 24").Column("Guarantee length").ClickLink();
            ExpectHeader("Guarantee Length");

            //Create new Guarantee Length
            ClickLink("New Guarantee Length");
            Set("Valid for (days)").To("20");
            ClickButton("Save");

            //Asserts new Garantee Length has been saved
            ExpectRow("20");
            ClickLink("Authorised Locations");
            ExpectHeader("Authorised Locations");
            AtRow("Stop 24").Column("Guarantee length").Expect("2");
            AtRow("Stop 24").Column("Guarantee length").ClickLink();
            ExpectHeader("Guarantee Length");

            //Archive the new Authorised Location
            AtRow("20").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectNoRow("20");

            //Assert that Authorised Location count has dropped
            ClickLink("Authorised Locations");
            ExpectHeader("Authorised Locations");
            AtRow("Stop 24").Column("Guarantee length").Expect("1");
        }
    }
}