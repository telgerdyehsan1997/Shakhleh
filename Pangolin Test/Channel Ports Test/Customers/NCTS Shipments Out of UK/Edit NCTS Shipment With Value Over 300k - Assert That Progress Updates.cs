using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditNCTSShipmentWithValueOver300k_AssertThatProgressUpdates : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOut_CommodityValueOver300000>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS Shipments
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date Created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow("1000000");

            //Edits the Value of the Consigment and Commodity to be below 300001
            AtRow("1000000").Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            //ExpectHeader("Consignments");
            AtRow("CP100000001").Column("Edit").Click("Edit");
            Set("Total value").To("1");
            Click("Save and Add Commodities");

            AtRow("12121212 - 14").Column("Edit").Click("Edit");
            ExpectHeader("Commodity Details");
            Set("Value").To("1");
            Click("Save");

            //Assert that Progress of Shipment Progress has updated
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date Created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");
            ExpectRow("1000000");
            AtRow("1000000").Column("Progress").Expect("Draft - Normal");
        }
    }
}