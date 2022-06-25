using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditAndTransmitEADOutOfUK_ASMACCEPTED : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentsForTruckersLimitedGBP, AddCommoditiesForTruckersLTDGBP>();
            //Run<AdminAddsExchangeRates>();

            LoginAs<ChannelPortsAdmin>();

            var dateToday = DateTime.Now;

            var UniqueUCR = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();

            //edit UCR to be unique


            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("T0721000001");
            AtRow("T0721000001").Column("Consignments").ClickLink("1");
            AtRow("T072100000101").Click("Edit");
            ExpectHeader("Consignment Details");

            // Set("UCR").To("9GBGB222333444555-T" + UniqueUCR);
            Click("Save and add Commodities");


            Click("Transmit");
            Click("Back to shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("T0721000001");
            AtRow("T0721000001").Column("Tracking number").ClickLink("T0721000001");
            ExpectHeader("Shipment Details");
            AtRow("T072100000101").Expect("Ready to transmit");
            Click(The.Top, "Transmit");
            //AtRow("T071900000101").Column("Progress").Expect("ASM Accepted");
            //Nothing will occur after Transmit as testing locally can not transmit to ASM
        }
    }
}
