using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Add2ndConsignmentswithonecommodity : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126869")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0120000001";

            Run<AddNewShipmentForTruckersLtd, AddConsignmentswithonecommodity>();
            //login as admin
            LoginAs<ChannelPortsAdmin>();

            // Navigation
            Click("Shipments");
            WaitToSeeHeader("Shipments");
            this.FindShipment(trackingNumber);
            AtRow(That.Contains, "R0120000001").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            Click("New Consignment");

            ExpectHeader("Consignment Details");
            Set("UK Trader").To("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Click(The.Bottom, "TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            ClickLabel("Partner name");
            Click(The.Bottom, "TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            ClickLabel("Declarant");
            Click(The.Bottom, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            Set("Total packages").To("5");
            Set("Total gross weight").To("7.89");
            Set("Total net weight").To("6.232");
            Set("Invoice number").To("TRUCKERS-2019-1102");
            Set("Invoice currency").To("GBP");
            Click(What.Contains, "GBP");
            Set("Total value").To("400");
            ClickLabel("Only 1 Commodity");
            Click(What.Contains, "Save and Add Commodities");

            ExpectHeader("Commodity Details");
            ClickLabel("Product Code");
            Click("iPod 64GB - ABS00004");
            AtField("Gross weight").ExpectValue("7.89");
            AtField("Net weight").Expect("6.232");
            RightOf(The.Top, "Second quantity").ExpectText(That.Contains, "025");
            Set("Second quantity").To("100");
            ExpectNoField("Third quantity");
            Set("Country of origin").To("FR - FRANCE");
            Click(The.Bottom, " FR - FRANCE");
            Click("Save");

            ExpectHeader(That.Contains, "R012000000102 - Commodities");
            //ExpectRowColumns(That.Contains, "ABS00004", "7.89 kg", "6.23 kg", "GBP", "400", "", "FRANCE", "No");

        }
    }
}
