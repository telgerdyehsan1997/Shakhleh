using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetCompanyAPISettingsWWL : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Column("Company name").ClickLink("");
            Expect("API Settings");
            Click("API Settings");
            // ----------------------------------------------

            WaitToSeeHeader("API Settings");
            ExpectField("Username");
            ExpectField("Password");
            ExpectField("Primary Contact");

            Click("Save");

            Expect(What.Contains, "The Username field is required.");
            Expect(What.Contains, "The Password field is required.");
            Expect(What.Contains, "The Primary Contact field is required.");

            Set("Username").To("Invalid username");
            Set("Password").To("API_PassWord");
            ClickField("Primary contact");
            Type("Jenny");
            Click(The.Bottom, "Jenny Smith");
            Click("Save");

            Expect(What.Contains, "Username should be a valid Email address.");

            Set("Username").To("WWL_API_User@uat.co");
            Click("Save");
        }
    }
}
