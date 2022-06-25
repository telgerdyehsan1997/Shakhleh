using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertCCEmailsReceiveResponseNotification : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.CustomerSupportTicket);

            Run<SupportTicketRaisedWithCCAdded>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Dashboard
            this.NavigateToCustomerDashboard();

            ExpectRow(ticket.TrackingNumber);
            AtRow(ticket.TrackingNumber).Column("Responses").ClickLink();

            //Raises the new response
            ExpectHeader($"{ticket.TrackingNumber} Responses");
            ClickLink("New Response");
            ExpectHeader("Response details");
            Set("Message").To("Company Response");
            ClickButton("Send");

            //Asserts that Response has been raised
            ExpectRow("Company Response");

            //Checks if Ticket Notification has been added to those CC'd in
            CheckMailBox("");
            ExpectRow("TEST1@UAT.CO");
            ExpectRow("TEST2@UAT.CO");
            ExpectRow("TEST3@UAT.CO");
        }
    }
}