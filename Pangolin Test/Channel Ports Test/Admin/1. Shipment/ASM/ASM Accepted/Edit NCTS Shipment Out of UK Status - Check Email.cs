using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditNCTSShipmentOutOfUKStatus_CheckEmail : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipentNCTSOutOfUKASMAcc, EnableStatusEmailNotifications_Customer>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS out
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("1000000").Column("Tracking number").Click("1000000");
            ExpectHeader("Shipment details");
            AtRow("CP100000101").Column("Progress").Click("Ready to Transmit");
            ExpectHeader("Progress History");
            Set("Progress").To("OnHoldValue");
            ClickButton("Save");
            CheckMailBox("");
        }
    }
}