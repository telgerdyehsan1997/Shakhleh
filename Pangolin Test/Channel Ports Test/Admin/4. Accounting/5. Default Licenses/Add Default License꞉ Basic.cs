using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddDefaultLicenseBasic : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            Click("Accounting");
            ExpectHeader("VAT Rates");
            Click("Default Licenses");

            ExpectHeader("Default Licenses");
            Click("New Default License");
            ExpectHeader("Default License Details");

            Set("Name").To("Basic");
            AtLabel("Currency").ClickLabel("Euro");
            //AtLabel("Currency").ExpectChoose("Euro");
            Set("License Fee").To("50");
            //AtLabel("Free consignments frequency").ExpectChoose("Monthly");
            //Set("Free Consignments (Monthly)").To("3");
            //Set("Free Consignments (Yearly)").To("5");
            Set("Number of Free Consignments").To("3");
            Set("Price Per Additional Consignment").To("50");
            Set("Price Per Commodity").To("24.99");

            ClickButton("Save");

            ExpectHeader("Default Licenses");
            ExpectRow("Basic");
            AtRow("BASIC").Column("Name").Expect("BASIC");
            AtRow("BASIC").Column("License Fee").Expect("50.00");
            AtRow("BASIC").Column("Free Consignments").Expect("3");
            AtRow("BASIC").Column("Price Per Additional Consignment").Expect("50.00");
            AtRow("BASIC").Column("Price Per Commodity").Expect("24.99");
            AtRow("BASIC").Column("Currency").Expect("Euro");
        }
    }
}