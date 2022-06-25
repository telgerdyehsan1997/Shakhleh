using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class APIUsernameIsUnique : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<SetCompanyAPISettingsWWL, AdminAddsCompanyTruckersLtd, AddNewContactForTruckers_AlanSmith>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Column("Company name").ClickLink("");
            Expect("API Settings");
            Click("API Settings");
            // ----------------------------------------------

            Set("Username").To("WWL_API_User@uat.co");
            Set("Password").To("API_PassWord");
            ClickField("Primary contact");
            Type("Alan");
            Click(The.Bottom, "Alan Smith");
            Click("Save");

            Expect(What.Contains, "Username must be unique. There is an existing Company record with the provided Username.");
            Click("OK");

            Set("Username").To("Truckers_API_User@uat.co");
            Click("Save");

        }
    }
}
