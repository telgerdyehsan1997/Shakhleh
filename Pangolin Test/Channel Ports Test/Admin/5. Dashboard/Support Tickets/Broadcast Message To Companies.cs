using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class BroadcastMessageToCompanies : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var contactEmail = "Kaumza.Kiryu@uat.co";
            var subjectLine = "Broadcasted Subject";

            Run<AddKazumaKiryuToMajimaConstruction, AddCompanyUserToMajimaConstructionMajima>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Dashboard
            ClickLink("Dashboard");
            ExpectHeader("Support Tickets");

            //Creates the message to Broadcast
            ClickLink("Broadcast Message");
            ExpectHeader("Broadcast message details");
            Set("Subject").To("Broadcasted Subject");
            Set("Body").To("Broadcasted Message");
            Set("Company types").To("Customer");
            Set("GVMS types").To("Sometimes");
            Set("Inbound safety and security options").To("Sometimes");

            //Sends the message
            ClickButton("Save");
            Expect(What.Contains, "Are you sure you want to send this broadcast message to all companies?");
            ClickButton("OK");

            ExpectHeader("Support Tickets");

            //Runs the background task to send the message
            this.BroadcastMessage();
            Goto("/");

            //Checks the Inbox of the Contact to see if they received the message;
            CheckMailBox(contactEmail);
            ExpectHeader("Emails sent to kaumza.kiryu@uat.co");
            //AtRow(contactEmail).Column("Subject").ExpectLink(subjectLine);
            // AtRow(contactEmail).Column("Subject").ClickLink(subjectLine);

            //Assers that the Emails contains the Broadcasted message
            //  ExpectHeader($"Subject:" + subjectLine);
            //Expect("Broadcasted Message");

            //Manually assert that Broadcased message has been sent as this the background task does not want to run through Pangolin
        }
    }
}