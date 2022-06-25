using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditProducts : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsProduct_IPad>();

            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");
            WaitToSeeHeader("Products");

            //edit item
            AtRow(That.Contains, "ABS12343").Column("Edit").Click("Edit");
            WaitToSeeHeader("Product Details");
            Set("Product code").To("ABS12344");
            Set("Product name/Description of goods").To("IPhone 4");
            Set("Commodity code").To("");
            ClickHeader("Product Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Click("12345678 - 14");
            Set("Additional code").To("5555");
            Set("Quota").To("777777");
            Set("Export licence").To("");
            Click("Save");
            WaitToSeeHeader("Products");

            //assert new details
            AtRow(That.Contains, "ABS12344").Column("Product code").Expect("ABS12344");
            AtRow(That.Contains, "ABS12344").Column("Product name").Expect("IPhone 4");
            AtRow(That.Contains, "ABS12344").Column("Commodity code").Expect("12345678 - 14");
            AtRow(That.Contains, "ABS12344").Column("Addl code").Expect("5555");
            AtRow(That.Contains, "ABS12344").Column("Quota").Expect("777777");
            AtRow(That.Contains, "ABS12344").Column("Second quantity").ExpectText("");
            AtRow(That.Contains, "ABS12344").Column("Third quantity").ExpectText("");
            AtRow(That.Contains, "ABS12344").Column("Licenced").ExpectTick();
        }
    }
}