using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddVatRate_Test : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            LoginAs<ChannelPortsAdmin>();
            Click("Accounting");
            Click("Vat Rates");
            ExpectHeader("Vat Rates");

            Click("New Vat Rate");
            ExpectHeader("VAT Rate Details");

            Set("Valid From").To("01/01/2022");
            Set("VAT Rate Name").To("Test VAT Rate Name");
            Set("VAT Rate for S").To("1");
            Set("VAT Rate for Z").To("2");
            Set("VAT Rate for A").To("3");
            Set("Statement").To("Test Statement");
            Click("Save");

            ExpectRow("TEST VAT RATE NAME");

        }
    }
}