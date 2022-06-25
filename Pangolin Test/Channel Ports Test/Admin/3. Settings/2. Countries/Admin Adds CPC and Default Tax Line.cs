using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsCPCAndDefaultTaxLine : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            Click("Settings");
            Click("CPC");

            ClickLink("New CPC");
            ExpectHeader("CPC Details");

            Set("Type").To("Into UK");
            Set("CPC Number").To("6667666");
            Set("CPC description").To("Test CPC - with no Tax line");
            Click("Save");

            ClickLink("Default Tax Lines");
            ExpectHeader("Default Tax Lines");
            ClickLink("New Default Tax Line");

            ExpectHeader("Default Tax Line Details");
            AtLabel("Tax Type").ClickLabel("A");
            AtLabel("Tax Type").ExpectChoose("A");
            Set("Tax Type Suffix").To("Test default tax");
            Set("Base Amount").To("10");
            Set("Base Quantity").To("9");
            Set("Rate").To("8");
            Set("Override").To("7");
            Set("Amount").To("20");
            Set("MoP").To("12");
            ClickButton("Save");

            ExpectRow("ATEST DEFAULT TAX");
            AtRow("ATEST DEFAULT TAX").Column("Type").Expect("ATEST DEFAULT TAX");
            AtRow("ATEST DEFAULT TAX").Column("Base Amount").Expect("10");
            AtRow("ATEST DEFAULT TAX").Column("Base Quantity").Expect("9");
            AtRow("ATEST DEFAULT TAX").Column("Rate").Expect("8");
            AtRow("ATEST DEFAULT TAX").Column("Override").Expect("7");
            AtRow("ATEST DEFAULT TAX").Column("Amount").Expect("20");
            AtRow("ATEST DEFAULT TAX").Column("MoP").Expect("12");
        }
    }
}