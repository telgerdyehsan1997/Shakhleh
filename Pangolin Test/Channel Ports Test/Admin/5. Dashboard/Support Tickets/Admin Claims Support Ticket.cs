using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminClaimsSupportTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Tickets(Tickets.CustomerSupportTicket);

            Run<GoroMajimaRaisesSupportTicket>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Support Tickets
            this.NavigateToSupportTickets();
            ExpectRow(ticket.TrackingNumber);

            //Claims the Support Ticket
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            Expect("Claim");
            System.Threading.Thread.Sleep(1000);
            Click("Claim");

            //Asserts that Support Ticket has been claimed
            AtRow(ticket.TrackingNumber).Column("Claimed By").Expect("Geeks Admin");
            AtRow(ticket.TrackingNumber).Column("Actions").Expect("Select action");
        }
    }
}