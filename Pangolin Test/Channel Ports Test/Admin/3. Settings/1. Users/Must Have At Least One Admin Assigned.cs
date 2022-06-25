using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class MustHaveAtLeastOneAdminAssigned : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewNon_AdminUser_NormanFreeman>();
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            //navigate
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            ExpectRow("admin@uat.co");
            ExpectRow("nfreeman@uat.co");

            AtRow("admin@uat.co").Column("Admin").ExpectTick();
            AtRow("nfreeman@uat.co").Column("Admin").ExpectNoTick();

            //click Edit on User
            AtRow(That.Contains, "admin@uat.co").Column(That.Contains, "Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "User Details");

            // ----------------------------------------------

            //change details - Attempt to unassign remaining Admin.
            AtLabel("Admin").ClickLabel("No");
            Click("Save");

            Expect("At least one of the ChannelPorts users should be in Admin role.");
            Click("Ok");
            Click("Cancel");

            AtRow(That.Contains, "nfreeman@uat.co").Column(That.Contains, "Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "User Details");

            // ----------------------------------------------

            //change details - Assign non-Admin as Admin.
            AtLabel("Admin").ClickLabel("Yes");
            Click("Save");

            AtRow("nfreeman@uat.co").Column("Admin").ExpectTick();

            //click Edit on User
            AtRow(That.Contains, "admin@uat.co").Column(That.Contains, "Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "User Details");

            // ----------------------------------------------

            //change details
            AtLabel("Admin").ClickLabel("No");
            Click("Save");

            ExpectNo("At least one of the ChannelPorts users should be in Admin role.");
            AtRow("admin@uat.co").Column("Admin").ExpectNoTick();
        }
    }
}