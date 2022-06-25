using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Non_AdminCannotSeeAdminSections : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewNon_AdminUser_NormanFreeman>();

            LoginAs<ChannelPortsAdmin>();

            //navigate
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Settings");

            CopyUrl();

            // Check that Non-Admin cannot access Settings page
            LoginAs<NormanNon_Admin>();

            WaitToSeeHeader(That.Contains, "Shipment");
            ExpectNo("Settings");

            GotoCopiedUrl();

            ExpectNoHeader("Settings");

            LoginAs<ChannelPortsAdmin>();

            //navigate
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Settings");

            Click("Users");

            AtRow("admin@uat.co").Click("Edit");

            WaitToSeeHeader("User Details");

            CopyUrl();

            // Check that Non-Admin cannot access User edit page
            LoginAs<NormanNon_Admin>();

            WaitToSeeHeader(That.Contains, "Shipment");

            GotoCopiedUrl();

            ExpectNoHeader("User Details");
        }
    }
}