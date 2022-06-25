using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UDUsersPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Users";

            LoginAs<Undischarged_ChannelPortsAdmin>();

            //Navigates to Users
            this.NavigateToUDSettings(settingsPage);

            //Asserts correct fields are present
            ExpectField("Name");
            ExpectField("Email");

            //Asserts correct radio Buttons are present
            ExpectLabel("Status");
            AtLabel("Status").Expect("Active");
            AtLabel("Status").Expect("Archived");
            AtLabel("Status").Expect("All");

            //Asserts columns
            ExpectColumn("Name");
            ExpectColumn("Email");
            ExpectColumn("Admin");
            ExpectColumn("Edit");
            ExpectColumn("Archive");

            //Asserts Links/Buttons
            ExpectLink("New User");
            ExpectButton("Search");
        }
    }
}