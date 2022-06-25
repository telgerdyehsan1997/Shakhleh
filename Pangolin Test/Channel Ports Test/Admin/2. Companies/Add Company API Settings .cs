using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyAPISettings : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewContactForTruckers_AlanSmith>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            Click("Truckers Ltd");
            Click("API Settings");

            // ----------------------------------------------

            //edit details
            Set("Username").To("John@uat.co");
            Set("Password").To("test");
            Set("Primary contact").To("Alan");
            Click("Alan Smith");
            Click("Save");

            // ----------------------------------------------

            AtLabel("Username").ExpectValue("John@uat.co");
            AtLabel("Password").ExpectValue("test");
            AtLabel("Primary contact").ExpectValue("Alan Smith");
        }

    }
}