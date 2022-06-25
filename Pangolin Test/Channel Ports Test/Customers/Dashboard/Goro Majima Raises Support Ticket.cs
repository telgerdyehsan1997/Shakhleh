using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GoroMajimaRaisesSupportTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Constants.TicketFactory().AddTicketForMajimaShipment();

            Run<NewShipmentOutOfUK_SafetyAndSecurityEnabled>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Shipments Out of the UK
            ClickLink("Shipments Out of UK");

            //Finds the Shipment
            this.FindShipment(ticket.TrackingNumber);

            //Raises the Support Ticket
            AtRow(ticket.TrackingNumber).Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(100);
            Click("Raise Support Ticket");

            //Sets the Support Ticket Details
            ExpectHeader("Raise Support Request");
            AtLabel("Full Name").Expect(ticket.FullName);
            AtLabel("Email").Expect(ticket.Email);
            AtLabel("Phone").Expect(ticket.Phone);
            AtLabel(The.Right, "Tracking Number").Expect(ticket.TrackingNumber);
            Set("Details").To(ticket.Details);
            ClickButton("Save");
            ExpectHeader("Shipments Out of UK");

            //Navigates to Support Tickets
            this.NavigateToCustomerDashboard();

            //Asserts that the Support Ticket has been raised
            ExpectRow(ticket.TrackingNumber);
            AtRow(ticket.TrackingNumber).Column("Task").Expect(ticket.Details);
        }
    }
}