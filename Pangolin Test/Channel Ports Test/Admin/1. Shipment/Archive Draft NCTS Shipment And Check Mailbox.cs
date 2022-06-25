using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveDraftNCTSShipmentAndCheckMailbox : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "1000000";
            var archiveReason = "Archived NCTS Shipment";

            Run<AddDraftConsignmentToNCTSShipment>();

            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipment
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Search for NCTS Shipment
            Set("Date created").To("01/07/2021");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow(trackingNumber);

            //Archive NCTS Shipment
            AtRow(trackingNumber).Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To(archiveReason);
            ClickButton("Archive");
            ExpectNoRow(trackingNumber);

            //Checks to see if Draft Archive Email has been sent
            CheckMailBox("customspro@channelports.co.uk");
        }
    }
}