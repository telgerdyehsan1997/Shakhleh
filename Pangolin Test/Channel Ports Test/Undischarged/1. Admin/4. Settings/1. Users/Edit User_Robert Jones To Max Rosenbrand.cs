using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class EditUser_RobertJonesToMaxRosenbrand : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewUndischargedUser_RobertJones>();

            LoginAs<Undischarged_ChannelPortsAdmin>();

            // ----------------------------------------------

            //click Edit on User
            ExpectHeader("Undischarged NCTS");
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            AtRow(That.Contains, "robert.jones@uat.co").Column(That.Contains, "Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "User Details");

            // ----------------------------------------------

            //assert layout on edit screen
            BelowHeader(That.Contains, "User Details").ExpectLabel(That.Contains, "First name");
            BelowLabel(That.Contains, "First name").ExpectLabel(That.Contains, "Last name");
            BelowLabel(That.Contains, "Last name").ExpectLabel(That.Contains, "Email");
            BelowLabel(That.Contains, "Email").ExpectLabel("Admin");

            // ----------------------------------------------

            //change details - cancel
            Set("First name").To("Kelly");
            Set("Last name").To("Green");
            AtLabel("Admin").ClickLabel("Yes");
            Click("Cancel");

            // ----------------------------------------------

            //expect unchanged details still associated with unchanged email address
            WaitToSeeHeader(That.Contains, "Users");
            AtRow("robert.jones@uat.co").Column("First name").ExpectNo("Kelly");
            AtRow("robert.jones@uat.co").Column("Last name").ExpectNo("Green");
            AtRow("robert.jones@uat.co").Column("Email").ExpectNo("kelly@uat.co");
            AtRow("robert.jones@uat.co").Column("Admin").ExpectTick();
            ExpectNoRow(That.Contains, "kelly@uat.co");


            AtRow(That.Contains, "robert.jones@uat.co").Column(That.Contains, "Edit").Click("Edit");
            Set("First name").To("Kelly");
            Set("Last name").To("Green");
            AtLabel("Admin").ClickLabel("Yes");
            Click("Save");
        }
    }
}