using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_EditVATRate : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var type = "VAT";
            var rate = "99%";
            var vatType = "Exempt";

            LoginAs<Undischarged_ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("VAT Rates");
            WaitToSeeHeader("Charge Types");

            ExpectRow(type);

            AtRow(type).Column("Edit").Click("Edit");
            WaitToSeeHeader("Charge Type Details");

            Set("Rate").To(rate);
            Set("VAT Type").To(vatType);
            Click("Save");

            AtRow(type).Column("Rate").Expect(rate);
            AtRow(type).Column("VAT Type").Expect(vatType);
        }
    }
}
