using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerAddsTicketForExistingTrackingNumberViaDashboard_ClosedPreviously : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var ticket = new Constants.TicketFactory().AddTicketForMajimaShipment();

            Run<AdminClosesSupportTicket>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Dashboard
            ClickLink("Dashboard");
            ExpectHeader("Dashboard");

            //Attempts to raise the duplicate ticket
            Click("Raise Support Request");
            ExpectHeader("Raise Support Request");
            AtLabel("Full Name").Expect(ticket.FullName);
            AtLabel("Email").Expect(ticket.Email);
            AtLabel("Phone").Expect(ticket.Phone);
            Set("Tracking Number").To(ticket.TrackingNumber);
            Set("Details").To(ticket.Details);
            Click("Save");

            //Assert duplicate ticket validation
            Expect(What.Contains, "A Closed Ticket for this Tracking number already Exists. Would you like to Re-open this ticket to add a Response?");
            ClickButton("Yes");
            ExpectHeader(That.Contains, "Responses");
        }
    }
}