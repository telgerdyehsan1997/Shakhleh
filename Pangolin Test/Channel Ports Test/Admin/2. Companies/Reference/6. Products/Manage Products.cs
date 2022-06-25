using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageProducts : UITest
    {
        [TestProperty("Sprint", "1")]
        [TestCategory("Bulk Upload To Be Added")]

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

            //assert list layout

            // UNCOMMENT WHEN BULK UPLOAD HAS BEEN ADDED
            //RightOfHeader("Products").Expect("Bulk Upload");
            //RightOf("Bulk Upload").Expect("New Product");

            RightOfHeader("Products").Expect("New Product");
            BelowHeader("Products").Expect("Status");
            RightOf("Status").ExpectButton("Search");

            //assert 'new item' screen layout
            Click("New Product");
            WaitToSeeHeader("Product Details");
            BelowHeader("Product Details").Expect("Product code");
            Below("Product code").Expect("Product name/Description of goods");
            Below("Product name/Description of goods").Expect("Commodity code");
            Below("Commodity code").Expect("Additional code");
            Below("Additional code").ExpectLabel("Quota");
            BelowLabel("Quota").ExpectLabel("Second quantity");
            BelowLabel("Second quantity").ExpectLabel("Third quantity");
            BelowLabel("Third quantity").Expect("VAT");
            Below("VAT").Expect("Licenced");
            AtLabel("Licenced").ClickLabel("Yes");
            Below("Licenced").Expect("Export licence");
            NearButton("Cancel").ExpectButton("Save");
            Click("Cancel");

            // UNCOMMENT WHEN BULK UPLOAD HAS BEEN ADDED
            //assert 'bulk upload' screen layout
            /*WaitToSeeHeader("Products");
            Click("Bulk Upload");
            WaitToSeeHeader("Bulk Upload");
            BelowHeader("Bulk Upload").Expect("Upload file");
            Near("Upload file").ExpectButton("Choose file");
            Below("Upload file").ExpectButton("Cancel");
            NearButton("Cancel").ExpectButton("Upload");
            Click("Cancel");
            WaitToSeeHeader("Products");*/

            //check search works
            ClickLabel("All");
            Click("Search");
            ExpectRow(That.Contains, "ABS12343");
            ClickLabel("Active");
            Click("Search");
            ExpectRow(That.Contains, "ABS12343");
            ClickLabel("Archived");
            Click("Search");
            ExpectNoRow(That.Contains, "ABS12343");

            //archive item
            ClickLabel("All");
            Click("Search");
            AtRow(That.Contains, "ABS12343").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");


            //check search works after archive
            ClickLabel("All");
            Click("Search");
            ExpectRow(That.Contains, "ABS12343");
            ClickLabel("Active");
            Click("Search");
            ExpectNoRow(That.Contains, "ABS12343");
            ClickLabel("Archived");
            Click("Search");
            ExpectRow(That.Contains, "ABS12343");
        }
    }
}