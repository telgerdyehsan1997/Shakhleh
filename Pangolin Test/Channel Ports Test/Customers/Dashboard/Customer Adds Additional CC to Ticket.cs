using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerAddsAdditionalCCToTicket : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";

            Run<SupportTicketRaisedWithCCAdded>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Dashboard
            ClickLink("Dashboard");

            //Checks the CC's for the Ticket
            ExpectHeader("Dashboard");
            AtRow(trackingNumber).Column("CC").Expect("2");
            AtRow(trackingNumber).Column("CC").ClickLink("2");
            ExpectHeader("CC's");
            Expect("TEST1@UAT.CO");
            Expect("TEST2@UAT.CO");

            //Removes one of the CC's
            Click("AddAnother");
            Type("TEST3@UAT.CO");
            ClickButton("Save");

            ExpectHeader("Dashboard");
            AtRow(trackingNumber).Column("CC").Expect("3");
        }
    }
}