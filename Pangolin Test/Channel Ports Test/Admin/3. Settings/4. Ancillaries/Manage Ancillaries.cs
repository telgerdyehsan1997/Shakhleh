using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageAncillaries : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<SetAncillariesForUK>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            Click("Settings");
            WaitForNewPage();
            Expect("Ancillaries");
            Click("Ancillaries");
            WaitToSeeHeader("Ancillaries");
            ExpectRow("GB");

            // ----------------------------------------------

            // Edit - Cancel
            AtRow("GB").Column("Edit").Click("Edit");
            WaitToSeeHeader("Freight Charge Details");
            AtLabel("Country").Expect("GB");
            Set("Freight charge per tonne").To("100");
            Set("Full load freight charge").To("150");
            Set("Value for VAT").To("50");
            Click("Cancel");

            // ----------------------------------------------

            AtRow("GB").Column("Freight charge per tonne").Expect("£100.00");
            AtRow("GB").Column("Full load freight charge").Expect("£150.00");
            AtRow("GB").Column("Value for VAT").Expect("£50.00");

            // ----------------------------------------------

            // Edit - Save
            AtRow("GB").Column("Edit").Click("Edit");
            WaitToSeeHeader("Freight Charge Details");
            AtLabel("Country").Expect("GB");
            Set("Freight charge per tonne").To("200");
            Set("Full load freight charge").To("250");
            Set("Value for VAT").To("100");
            Click("Save");

            // ----------------------------------------------

            AtRow("GB").Column("Freight charge per tonne").Expect("£200.00");
            AtRow("GB").Column("Full load freight charge").Expect("£250.00");
            AtRow("GB").Column("Value for VAT").Expect("£100.00");

            // ----------------------------------------------

            // Find
            Set("Find").To("GB");
            Click("Search");

            ExpectRow("GB");

            Set("Find").To("US");
            Click("Search");

            ExpectNoRow("GB");
            ExpectNoRow("US");
        }
    }
}