using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddVATRatesStandardRate : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            WaitToSee("Shipments");
            ClickLink("Accounting");
            ExpectHeader("VAT Rates");
            Click("New VAT Rate");
            ExpectHeader("VAT Rate Details");
            Set("Valid from").To("20/10/2020");
            Set("VAT Rate Name").To("Standard Rate");
            Set("VAT Rate for S").To("10");
            Set("VAT Rate for Z").To("10");
            Set("VAT Rate for A").To("10");
            Click("Save");
            ExpectHeader("VAT Rates");
            ExpectRow("Standard Rate");

            //another rate added

            Click("New VAT Rate");
            ExpectHeader("VAT Rate Details");
            Set("Valid from").To("20/04/2022");
            Set("VAT Rate Name").To("Quaterly rate");
            Set("VAT Rate for S").To("12");
            Set("VAT Rate for Z").To("12");
            Set("VAT Rate for A").To("12");
            Click("Save");
            ExpectHeader("VAT Rates");
            ExpectRow("Quaterly rate");

            //another rate added

            Click("New VAT Rate");
            ExpectHeader("VAT Rate Details");
            Set("Valid from").To("20/10/2022");
            Set("VAT Rate Name").To("Yearly rate");
            Set("VAT Rate for S").To("15");
            Set("VAT Rate for Z").To("15");
            Set("VAT Rate for A").To("15");
            Click("Save");

            ExpectHeader("VAT Rates");
            ExpectRow("Yearly Rate");
        }
    }
}