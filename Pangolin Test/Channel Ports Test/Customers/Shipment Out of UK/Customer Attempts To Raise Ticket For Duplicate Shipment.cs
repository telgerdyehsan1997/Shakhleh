using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerAttemptsToRaiseTicketForDuplicateShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Constants.TicketFactory().AddTicketForMajimaShipment();

            Run<GoroMajimaRaisesSupportTicket>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Shipments Out of the UK
            ClickLink("Shipments Out of UK");

            //Finds the Shipment
            this.FindShipment(ticket.TrackingNumber);

            //Raises the Support Ticket
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            Click("Raise Support Ticket");

            //Sets the Support Ticket Details
            ExpectHeader("Raise Support Request");
            AtLabel("Full Name").Expect(ticket.FullName);
            AtLabel("Email").Expect(ticket.Email);
            AtLabel("Phone").Expect(ticket.Phone);
            AtLabel(The.Right, "Tracking Number").Expect(ticket.TrackingNumber);
            Set("Details").To(ticket.Details);
            Click("Save");
            Expect(What.Contains, "A Ticket for this Tracking Number already exists. Would you like to add a Response to the Ticket?");
            Click("Yes");
            ExpectHeader(That.Contains, "Responses");
        }
    }
}