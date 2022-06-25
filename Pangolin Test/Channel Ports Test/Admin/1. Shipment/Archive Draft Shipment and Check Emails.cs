using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveDraftShipmentAndCheckEmails : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var archiveReason = "Archived Shipment";

            Run<AddDraftConsignmentToDraftShipment>();

            LoginAs<ChannelPortsAdmin>();

            //Search for draft shipment
            Set("Date created").To("01/07/2021");
            Set("Expected date of arrival/departure").To("03/08/2021");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow(trackingNumber);

            //Archive the Shipment
            AtRow(trackingNumber).Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To(archiveReason);
            ClickButton("Archive");
            ExpectNoRow(trackingNumber);

            //Assert that emails have been sent
            CheckMailBox("customspro@channelports.co.uk");
        }
    }
}