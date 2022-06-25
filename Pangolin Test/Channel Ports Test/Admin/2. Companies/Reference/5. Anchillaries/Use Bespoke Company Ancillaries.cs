using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCompanyAcillaries : UITest
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


            Set("Country").To("United Kingdom");
            AtLabel("Freight charge per tonne").Expect("100.00");
            AtLabel("Full load freight charge").Expect("150.00");
            AtLabel("Value for VAT").Expect("50.00");

            ExpectNo("Use Bespoke Rates");

            Click("Save");

            ExpectHeader("Ancillaries");
            ExpectRow("GB");

        }
    }
}
