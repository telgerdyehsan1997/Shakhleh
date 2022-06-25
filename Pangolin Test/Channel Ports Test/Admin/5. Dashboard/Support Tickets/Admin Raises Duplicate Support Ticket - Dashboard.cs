using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminRaisesDuplicateSupportTicket_Dashboard : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Constants.TicketFactory().AddTicketForMajimaShipment();

            Run<GoroMajimaRaisesSupportTicket>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Dashboard
            ClickLink("Dashboard");

            ExpectHeader("Support Tickets");
            ClickLink("Raise Support Request");

            ExpectHeader("Raise Support Request");
            this.ClickAndWait("Company Name", "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567");
            this.ClickAndWait("Full Name", "GORO MAJIMA");
            Set("Tracking Number").To(ticket.TrackingNumber);
            Set("Details").To("Duplicate Request Via Dashboard");
            ClickButton("Save");

            //Asserts duplicate Tracking Number Pop-up
            ExpectHeader("A Ticket for this Tracking Number already exists. Would you like to add a Response to the Ticket?");
            ClickButton("Yes");

            //Asserts that the Duplicate Ticket has been raised as a Response
            ExpectHeader($"{ticket.TrackingNumber} Responses");
            ExpectRow("Duplicate Request Via Dashboard");
        }
    }
}