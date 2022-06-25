using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EnableStatusEmailNotifications_Customer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Settings
            ClickLink("Settings");
            ExpectHeader("Users");
            Click("Status Email Notifications");
            ExpectLink("Customer");
            ClickLink("Customer");
            ExpectHeader("Customer Status Email Notifications - Shipments");

            //Sets the Email Notifications for NCTS Shipments
            AtRow(The.Top, "DraftNormal").ClickCheckbox();
            AtRow(The.Top, "DraftAuthLock").ClickCheckbox();
            AtRow(The.Top, "Queried").ClickCheckbox();
            AtRow(The.Top, "ASMAccepted").ClickCheckbox();
            AtRow(The.Top, "ASMRejected").ClickCheckbox();
            AtRow(The.Top, "Cancelled").ClickCheckbox();
            AtRow(The.Top, "Acknowledged").ClickCheckbox();
            AtRow(The.Top, "Accepted").ClickCheckbox();
            AtRow(The.Top, "Released").ClickCheckbox();
            AtRow(The.Top, "Discharged").ClickCheckbox();
            AtRow(The.Top, "ReadyToTransmit").ClickCheckbox();
            AtRow(The.Top, "AwaitingDeparture").ClickCheckbox();
            AtRow(The.Top, "Partial").ClickCheckbox();
            AtRow(The.Top, "HMRCApiAccepted").ClickCheckbox();
            AtRow(The.Top, "HMRCApiRejected").ClickCheckbox();
            AtRow(The.Top, "OnHold").ClickCheckbox();
            AtRow(The.Top, "OnHoldValue").ClickCheckbox();

            ClickButton(The.Top, "Update");
        }
    }
}