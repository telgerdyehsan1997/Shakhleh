using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerAttemptsToRaiseTicketForDuplicateShipmentThroughDashboard : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Constants.TicketFactory().AddTicketForMajimaShipment();

            Run<GoroMajimaRaisesSupportTicket>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Dashboard
            ClickLink("Dashboard");
            ExpectHeader("Dashboard");
            ExpectRow(ticket.TrackingNumber);

            //Attempts to raise the duplicate ticket
            Click("Raise Support Request");
            ExpectHeader("Raise Support Request");
            AtLabel("Full Name").Expect(ticket.FullName);
            AtLabel("Email").Expect(ticket.Email);
            AtLabel("Phone").Expect(ticket.Phone);
            Set(The.Right, "Tracking Number").To(ticket.TrackingNumber);
            Set("Details").To(ticket.Details);
            Click("Save");

            //Assert duplicate ticket validation
            Expect(What.Contains, "A Ticket for this Tracking Number already exists. Would you like to add a Response to the Ticket?");
            Click("Yes");
            ExpectHeader(That.Contains, "Responses");
        }
    }
}