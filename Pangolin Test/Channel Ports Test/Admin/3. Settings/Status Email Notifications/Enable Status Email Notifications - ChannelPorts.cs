using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EnableStatusEmailNotifications_ChannelPorts : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Settings
            ClickLink("Settings");
            ExpectHeader("Users");
            Click("Status Email Notifications");
            ExpectLink("Channelports");
            ClickLink("Channelports");
            ExpectHeader("Channelport Status Email Notifications - Shipments");

            //Sets the Email Notifications for NCTS Shipments
            AtRow("DraftNormal").Column("Recieve Email Notification").ClickCheckbox();
            AtRow("DraftAuthLock").Column("Recieve Email Notification").ClickCheckbox();
            AtRow("Queried").Column("Recieve Email Notification").ClickCheckbox();
            AtRow("ASMAccepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Cancelled").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Acknowledged").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Released").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Discharged").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "ReadyToTransmit").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "AwaitingDeparture").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Partial").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "HMRCApiAccepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "HMRCApiRejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "OnHold").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "OnHoldValue").Column("Recieve Email Notification").ClickCheckbox();
        }
    }
}