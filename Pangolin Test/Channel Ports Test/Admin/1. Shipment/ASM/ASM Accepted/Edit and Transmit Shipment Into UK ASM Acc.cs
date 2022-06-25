using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditAndTransmitShipmentIntoUKASMAcc : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentIntoUKASMAccepted>();
            LoginAs<ChannelPortsAdmin>();

            var dateToday = DateTime.Now;

            var UniqueUCR = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            //edit UCR to be unique

            WaitToSeeHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            //This workflow no longer exists
            /*AtRow("R0721000001").Click("1");
            AtRow("R072100000101").Click("Edit");
            ExpectHeader("Consignment Details");

            Set("UCR").To("9GBGB222333444555-T" + UniqueUCR);
            Click("Save and add Commodities");

            Click("Complete");

            Click("Back to shipments");

            Click("R0121000001"); */

            AtRow("R0721000001").Column("Progress").Expect("Ready to transmit");

            AtRow("R0721000001").Column("Tracking number").ClickLink();
            AtRow("R072100000101").Column("Transmit").ClickButton();
            //AtRow("R012100000101").Column("Progress").Expect("ASM Accepted");
        }
    }
}