using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GoroMajimaViewsAdminResponse : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "T0721000001";

            Run<AdminRespondsToCustomerTicket>();
            LoginAs<Goro_MajimaConstruction>();

            //Manually check that the Dashboard has a notification
            ClickLink("Dashboard");
            System.Threading.Thread.Sleep(1000);

            //Manually check that the notification symbol is no longer visible
            ExpectHeader("Dashboard");

            //Assert that Customer Can View response
            AtRow(trackingNumber).Column("Responses").Expect("1");
            AtRow(trackingNumber).Column("Responses").ClickLink("1");

            ExpectHeader($"{trackingNumber} Responses");
            AtRow("Geeks Admin").Column("Message").Expect("Admin Response");
        }
    }
}