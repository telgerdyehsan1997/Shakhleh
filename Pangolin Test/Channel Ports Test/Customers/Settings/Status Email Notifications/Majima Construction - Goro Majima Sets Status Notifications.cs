using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class MajimaConstruction_GoroMajimaSetsStatusNotifications : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyUserToMajimaConstructionMajima>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to 'Status Email Notifications'
            ClickLink("Settings");
            ExpectHeader("Record Details");
            ClickLink("Status Email Notifications");
            ExpectHeader("Status Email Notifications - Shipments");

            //Sets the Status Email Notification
            AtRow(The.Top, "Processing Error").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Awaiting Arrival").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - Route - ASM Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - CPC - ASM Rejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - General").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "With Importer").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "EntryControlled").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "LeftCountry").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - Route - ASM Rejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "ASM Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - License - ASM Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - CPC - ASM Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - License").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - CPC").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Duty Payment").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Queried").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - General - ASM Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Arrived").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Draft").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Partial").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "With Customs").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Ready to Transmit").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - License - ASM Rejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Awaiting Departure").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Ready to Transmit (API)").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Processing Error").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Cleared").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - General - ASM Rejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - Route").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Queried").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Manual - Quota").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "ASM Rejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "Cancelled").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Top, "InternalError").Column("Recieve Email Notification").ClickCheckbox();

            //Saves the notification options for Shipments
            ClickButton(The.Top, "Update");

            AtRow(The.Bottom, "On Hold - Due to Value").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Released").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Discharged").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "HMRC Api Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Awaiting Departure").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Cancelled").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "HMRC Api Rejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "On Hold - Authorised Location").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Draft - Normal").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "ASM Rejected").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Ready to Transmit").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Draft - Auth Loc").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Partial").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Acknowledged").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "ASM Accepted").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "EntryControlled").Column("Recieve Email Notification").ClickCheckbox();
            AtRow(The.Bottom, "Queried").Column("Recieve Email Notification").ClickCheckbox();

            //Saves the notification options for NCTS Shipments
            ClickButton(The.Bottom, "Update");

            ExpectHeader("Status Email Notifications - NCTS Shipments");
        }
    }
}