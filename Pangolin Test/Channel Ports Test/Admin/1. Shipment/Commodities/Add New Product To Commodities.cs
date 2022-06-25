using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewProductToCommodities : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsProduct_IPad, AddNewShipmentForTruckersLtd, AddConsignmentToTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();

            WaitToSeeHeader("Shipments");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow("01/07/2021").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click(What.Contains, "Save");
            WaitToSeeHeader(That.Contains, "Consignments");

            //create new commodity
            AtRow(That.Contains, "Truckers Ltd").Column("Commodities").ClickLink();
            WaitToSeeHeader(That.Contains, "Commodities");
            Click("New Commodity");
            WaitToSeeHeader(That.Contains, "Details");
            ClickField("Product code");
            Type("IPad");
            System.Threading.Thread.Sleep(1000);
            ClickText(That.Contains, "IPad");
            Set("Gross weight").To("10");
            Set("Net weight").To("8");
            Set("Value").To("499.99");
            ClickField("Country of origin");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "FR - FRANCE");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "FR - FRANCE");
            Set("Second quantity").To("1");
            AtLabel("Preference").ClickLabel("No");
            Expect(What.Contains, "You have declared the origin as being from the EU - originating in the EU are entitled to Preference claim. Duty will be payable if you answer NO to preference.");
            Click("OK");
            Click("Save");
            WaitToSeeHeader(That.Contains, "Commodities");

            //change the new commodity's product
            AtRow(That.Contains, "ABS12343").Column("Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "Details");
            RightOf("Product code").Click("AddProduct");
            WaitToSeeHeader(That.Contains, "Product Details");
            Set(That.Equals, "Code").To("ABS12322");
            Set(That.Equals, "Name").To("Computer");
            ClickField("Commodity code");
            Type("12345678");
            System.Threading.Thread.Sleep(500);
            Click(What.Contains, "14");
            Click(The.Top, "Save");
            WaitToSeeHeader(That.Contains, "Commodity Details");
            Set("Gross weight").To("20");
            Set("Net weight").To("15");
            Set("Value").To("750.5");
            Click("Save");

            ExpectRow("ABS12343");

            //test cases passes test

        }
    }
}
