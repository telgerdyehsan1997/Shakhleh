using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithAddsNewProductForCommodities : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd>();

            //navigate
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0119000001").Column("Edit").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            //WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Commodities").ClickLink("0");
            //WaitToSeeHeader(That.Contains, "R011900000101 - Commodities");

            Expect("Consignment total gross weight");
            Near("Consignment total gross weight").Expect("12 kg");

            Expect("Consignment total net weight");
            Near("Consignment total net weight").Expect("9 kg");

            Expect("Consignment total value");
            Near("Consignment total value").Expect("1,200");

            //create new product to be used for commodities
            Click("New Commodity");
            WaitToSeeHeader("Commodity Details");
            RightOfField("Product code").Click("AddProduct");
            WaitToSeeHeader(That.Contains, "Product Details");
            Set(That.Equals, "Code").To("ABS12322");
            Set(That.Equals, "Name").To("Computer");
            ClickField("Commodity code");
            Type("12345678");
            System.Threading.Thread.Sleep(500);
            Click(What.Contains, "14");
            Click(The.Top, "Save");
            WaitToSeeHeader(That.Contains, "Details");

            //assess new product created
            ClickField("Product code");
            Type("Computer");
            System.Threading.Thread.Sleep(1000);
            ExpectText(That.Contains, "Computer");

            //Check has been added to company in admin
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            ClickLink(That.Contains, "Truckers");
            Click("Products");
            ExpectRow(That.Contains, "Computer");
        }
    }
}