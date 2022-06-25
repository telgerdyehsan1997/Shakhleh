using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchivedProductNotVisibleInCommodityForm : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //need a consignment and product
            Run<AddConsignmentToTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            //check product is in new commodity form

            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow(That.Contains, "R0721000001").Column("Edit").Click("Edit");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R072100000101").Column("Commodities").ClickLink("0");
            //WaitToSeeHeader(That.Contains, "R071900000101 - Commodities");
            Click("New Commodity");
            WaitToSeeHeader("Commodity Details");
            ClickField("Product code");
            Type("ABS12343");
            Expect(What.Contains, "IPad");

            //archive product
            Click(The.Top, "Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");
            WaitToSeeHeader(That.Contains, "Products");
            AtRow(That.Contains, "IPad").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archived Commodity");
            ClickButton("Archive");
            WaitToSeeHeader(That.Contains, "Products");

            //check product is not in new commodity form

            Click("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            AtRow(That.Contains, "R0721000001").Column("Edit").Click("Edit");
            Click("Save and Add/Amend Consignments");

            AtRow("R072100000101").Column("Commodities").ClickLink("0");
            Click("New Commodity");
            WaitToSeeHeader("Commodity Details");
            ClickField("Product code");
            Type("ABS12343");
            ExpectNo(What.Contains, "IPad");
        }
    }
}