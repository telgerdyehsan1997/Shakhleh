using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminRespondsToCustomerTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.AdminSupportTicket);

            Run<AdminClaimsSupportTicket>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Support Tickets
            this.NavigateToSupportTickets();

            //Navigates to Ticket Responses
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Expect("Respond");
            System.Threading.Thread.Sleep(1000);
            Click("Respond");

            //Adds the Response
            ExpectHeader("Response details");
            Set("Message").To("Admin Response");
            ClickButton("Send");

            //Asserts that Response has been sent
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Responses");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Responses");

            ExpectHeader($"{ticket.TrackingNumber} Responses");
            AtRow("Geeks Admin").Column("Message").Expect("Admin Response");
        }
    }
}