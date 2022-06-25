using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsProductIPhone_Import : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");
            WaitToSeeHeader("Products");
            Click("New Product");
            WaitToSeeHeader("Product Details");

            // ----------------------------------------------

            // Create new product
            Set("Product code").To("ABS12345");
            Set(That.Contains, "Product name").To("iPhone");
            ClickField("Commodity code");
            Type("12345678");
            System.Threading.Thread.Sleep(1500);
            Click(What.Contains, "12345678 - 12");
            Set("Additional code").To("5555");
            Set("Quota").To("777777");
            AtLabel("Licenced").ClickLabel("No");

            Click("Save");
            WaitToSeeHeader("Products");

            // ----------------------------------------------

            //assert new product in list
            AtRow(That.Contains, "ABS12345").Column("Product code").Expect("ABS12345");
            AtRow(That.Contains, "ABS12345").Column("Product name").Expect("iPhone");
            AtRow(That.Contains, "ABS12345").Column("Commodity code").Expect("12345678 - 12");
            AtRow(That.Contains, "ABS12345").Column("Addl code").Expect("5555");
            AtRow(That.Contains, "ABS12345").Column("Quota").Expect("777777");
            AtRow(That.Contains, "ABS12345").Column("Second quantity").Expect("025");
            AtRow(That.Contains, "ABS12345").Column("Third quantity").ExpectText("");
            //AtRow(That.Contains, "ABS12345").Column("VAT").Expect("");
            AtRow(That.Contains, "ABS12345").Column("Licenced").ExpectNoTick();
        }
    }
}