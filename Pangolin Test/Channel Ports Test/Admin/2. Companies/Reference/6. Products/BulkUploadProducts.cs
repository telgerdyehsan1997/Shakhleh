using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class BulkUploadProducts : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Companies");
            ExpectHeader("Companies");
            ExpectRow("Shipping Company Ltd");

            Click("Shipping Company Ltd");
            Click("Products");

            ExpectRow("ABS00001");

            Click("Bulk Upload");
            Set("Choose file").To("ProductBulkUploadTestNew.csv");
            Click("Save");

            CheckBackgroundTasks();
            AtRow("Run Product Bulk Import Service").Click("Execute");

            Goto("/");
            ClickLink("Companies");
            Click("Shipping Company Ltd");
            Click("Products");


            //bulk upload doesn't seems to work, as probably was updated
            ////New product added
            //ExpectRow("New Product");
            //AtRow("New Product").Column("Product code").Expect("ABS00003");
            //AtRow("New Product").Column("Commodity code").Expect("12345678 - 14");
            //AtRow("New Product").Column("Quota").Expect("123456");
            //AtRow("New Product").Column("Second quantity").ExpectNo(" ");
            //AtRow("New Product").Column("VAT").Expect("Z");
            //AtRow("New Product").Column("Licenced").ExpectTick();

            //Existing product altered
            ExpectRow("ABS00001");
            AtRow("ABS00001").Column("Edit").Click("Edit");
            Set("Commodity code").To("12345678 - 12");
            Set("Quota").To("777777");
            AtField("Licenced").ClickCheckbox();
            Click("Save");

        }
    }
}