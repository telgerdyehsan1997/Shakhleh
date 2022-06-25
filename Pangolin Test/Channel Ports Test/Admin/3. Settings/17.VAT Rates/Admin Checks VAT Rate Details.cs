using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminChecksVATRateDetails : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddVATRatesStandardRate>();

            LoginAs<ChannelPortsAdmin>();

            ClickLink("Accounting");

            ExpectHeader("VAT rates");

            ExpectRow("Standard rate");
            ExpectRow("Quaterly rate");
            ExpectRow("Yearly rate");

            AtRow("Standard rate").Column("Edit").Click("Edit");
            ExpectHeader("VAT rate details");

            Set("VAT Rate Name").To("New Yearly Rate");
            Set("Statement").To("Edited");
            Click("Save");

            AtRow("New Yearly Rate").Column("Statement").Expect("Edited");

        }
    }
}