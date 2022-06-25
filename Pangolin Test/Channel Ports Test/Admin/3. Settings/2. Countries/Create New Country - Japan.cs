using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pangolin;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCountry_Japan : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Settings
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Countries");
            ExpectHeader("Countries");

            //Creates new Country
            ClickLink("New Country");
            ExpectHeader("Country Details");

            Set("Country").To("Japan");
            Set("Country code").To("JP");
            AtLabel("EU27").ClickLabel("No");
            AtLabel("Preference available").ClickLabel("No");
            AtLabel("MFN").ClickLabel("No");
            Set("Import CPC without preference").To("4000000");
            Set("Import CPC without preference declaration type").To("IM");
            Set("Import CPC without preference preference code").To("100");
            Set("Import CPC without preference Rate Code").To("F");
            Set("Export CPC without preference").To("1000001");
            Set("Export CPC without preference declaration type").To("EX");
            Set("Invoice declaration document type").To("9001");
            Set("Invoice declaration document type document status").To("AG");
            Set("Preference certificate number document type").To("N954");
            Set("Preference certificate number document type document status").To("AG");
            Set("Country dialling code").To("+81");
            ClickButton("Save");

            //Asserts that new Country has been created
            AtRow("Japan").Column("Country").Expect("Japan");
            AtRow("Japan").Column("Country code").Expect("JP");
        }
    }
}