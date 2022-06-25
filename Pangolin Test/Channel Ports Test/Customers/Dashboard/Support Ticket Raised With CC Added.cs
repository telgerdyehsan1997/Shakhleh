using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SupportTicketRaisedWithCCAdded : UITest
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
            System.Threading.Thread.Sleep(1000);
            Type(ticket.CarbonCopy);
            System.Threading.Thread.Sleep(1000);
            ClickButton("AddAnother");
            System.Threading.Thread.Sleep(1000);
            ClickField("Details");
            Press(Keys.Tab);
            Press(Keys.Tab);
            Press(Keys.Tab);
            Type("TEST2@UAT.CO");

            ClickButton("Save");
            ExpectHeader("Shipments Out of UK");

            //Checks if Ticket Notification has been added to those CC'd in
            CheckMailBox("");
            ExpectRow("TEST1@UAT.CO");
            ExpectRow("TEST2@UAT.CO");
        }
    }
}