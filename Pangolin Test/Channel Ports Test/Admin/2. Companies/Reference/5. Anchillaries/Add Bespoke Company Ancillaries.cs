using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddBespokeCompanyAncillaries : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "124703")]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyTruckersLtd,SetAncillariesForUK>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            Click("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Click("Truckers Ltd");

            Expect("Ancillaries");
            Click("Ancillaries");
            WaitToSeeHeader("Ancillaries");

            ExpectNo("Country");
            ExpectNo("Freight charge per tonne");
            ExpectNo("Full load freight charge");
            ExpectNo("Value for VAT");

            Click("Add Bespoke Rate");

            ExpectHeader("Ancillary Details");

            Set("Country").To("United Kingdom");
            Set("Freight charge per tonne").To("100");
            Set("Full load freight charge").To("150");
            Set("Value for VAT").To("50");
            //Set("Insurance charge").To("10.12345"); this has not been implemented

            Click("Save");
            AtRow("GB").Column("Freight charge per tonne").ExpectNo("");
            AtRow("GB").Column("Freight charge per tonne").Expect("£100.00");

            AtRow("GB").Column("Full load freight charge").ExpectNo("");
            AtRow("GB").Column("Full load freight charge").Expect("£150.00");

            AtRow("GB").Column("Value for VAT").ExpectNo("");
            AtRow("GB").Column("Value for VAT").Expect("£50.00");

            //AtRow("GB").Column("Insurance charge").ExpectNo("");
            //AtRow("GB").Column("Insurance charge").Expect("10.12345%");
        }
    }
}
