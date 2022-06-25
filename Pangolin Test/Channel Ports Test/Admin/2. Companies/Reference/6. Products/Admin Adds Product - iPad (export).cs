using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsProduct_IPad : UITest
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
            //WaitToSeeHeader(That.Contains, "Shipment");
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
            Set("Product code").To("ABS12343");
            Set("Product name/Description of goods").To("IPad");
            ClickField("Commodity code");
            Type("12345678");
            Click("12345678 - 12");
            Set("Additional code").To("4444");
            Set("Quota").To("666666");
            AtLabel("VAT").Click(What.Contains, "---Select---");
            Click("S", Casing.Exact);
            //Set("Licence override into UK").To("ABC12");
            //Set("Licence override out of UK").To("DEF34");

            ExpectNo("Export licence");
            AtLabel("Licenced").ClickLabel("Yes");
            ClickLabel("Licenced");
            Set("Export licence").To("3456");
            Click("Save");
            WaitToSeeHeader("Products");

            // ----------------------------------------------

            //assert new product in list
            AtRow(That.Contains, "ABS12343").Column("Product code").Expect("ABS12343");
            AtRow(That.Contains, "ABS12343").Column("Product name").Expect("IPad");
            AtRow(That.Contains, "ABS12343").Column("Commodity code").Expect("12345678 - 12");
            AtRow(That.Contains, "ABS12343").Column("Addl code").Expect("4444");
            AtRow(That.Contains, "ABS12343").Column("Quota").Expect("666666");
            AtRow(That.Contains, "ABS12343").Column("Second quantity").Expect("025");
            //AtRow(That.Contains, "ABS12343").Column("Third quantity").Expect("");
            AtRow(That.Contains, "ABS12343").Column("VAT").Expect("S");
            AtRow(That.Contains, "ABS12343").Column("Licenced").ExpectTick();
            //AtRow(That.Contains, "ABS12343").Column("Licence override into UK").Expect("ABC12");
            //AtRow(That.Contains, "ABS12343").Column("Licence override out of UK").Expect("DEF34");
        }
    }
}