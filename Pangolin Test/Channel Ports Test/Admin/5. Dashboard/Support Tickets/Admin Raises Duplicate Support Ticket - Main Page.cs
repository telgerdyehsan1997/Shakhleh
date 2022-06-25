using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminRaisesDuplicateSupportTicket_MainPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Constants.TicketFactory().AddTicketForMajimaShipment();

            Run<GoroMajimaRaisesSupportTicket>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(ticket.TrackingNumber);

            //Raises the Duplicate Support Ticket
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            ExpectLink("Raise Support Ticket");
            System.Threading.Thread.Sleep(1000);
            ClickLink("Raise Support Ticket");

            ExpectHeader("Raise Support Request");
            Set("Phone").To("07804655555");
            Set("Details").To("Duplicate Request");
            ClickButton("Save");

            //Asserts duplicate Tracking Number Pop-up
            ExpectHeader("A Ticket for this Tracking Number already exists. Would you like to add a Response to the Ticket?");
            ClickButton("Yes");

            //Asserts that the Duplicate Ticket has been riased as a Response
            ExpectHeader($"{ticket.TrackingNumber} Responses");
            ExpectRow("Duplicate Request");
        }
    }
}