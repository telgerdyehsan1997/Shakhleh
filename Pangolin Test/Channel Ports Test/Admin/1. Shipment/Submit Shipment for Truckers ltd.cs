using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SubmitShipmentForTruckersLtd : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithResolvesConsignmentAndCommodityMismatch>();

            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            ClickButton("Save and Add/Amend Consignments");
            AtRow("R011900000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");
            ClickButton("Save and Add Commodities");
            ExpectHeader(That.Contains, "R011900000101");
            Click("Complete");
            ExpectHeader(That.Contains, "Duty is Payable on one or more of the commodity codes");
            Goto("/");
            ExpectHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0119000001");
            AtRow("R0119000001").Column("Progress").Expect("Draft");

            /* Near("Into UK").ClickLabel("All");
             Near("Submitted").ClickLabel("Draft");
             Near("Archived").ClickLabel("All");
             Click("Search");
             WaitToSeeHeader("Shipments");
             ExpectNoRow(That.Contains, "R0119000001");
             Near("Into UK").ClickLabel("All");
             ClickLabel("Submitted");
             Near("Archived").ClickLabel("All");
             Click("Search");
             WaitToSeeHeader("Shipments");
             ExpectRow(That.Contains, "R0119000001");*/

        }
    }
}