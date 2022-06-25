using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCompanyAncillaries_TruckersLtd : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "124703")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ManageCompanyAcillaries>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");

            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Click("Truckers Ltd");

            Expect("Ancillaries");
            Click("Ancillaries");
            WaitToSeeHeader("Ancillaries");

            ExpectRow("GB");
            AtRow("GB").Column("Freight charge per tonne").Expect("£100.00");
            AtRow("GB").Column("Full load freight charge").Expect("£150.00");
            AtRow("GB").Column("Value for VAT").Expect("£50.00");
            //Insurance charge has not been removed
            //AtRow("GB").Column("Insurance charge").Expect("10.12345%");

            AtRow("GB").Column("Edit").Click("Edit");

            WaitToSeeHeader("Ancillary Details");
            Set("Freight charge per tonne").To("120");
            Set("Full load freight charge").To("100");
            Click("Save");

            ExpectRow("GB");
            AtRow("GB").Column("Freight charge per tonne").Expect("£120.00");
            AtRow("GB").Column("Full load freight charge").Expect("£100.00");
            AtRow("GB").Column("Value for VAT").Expect("£50.00");
            //Insurance charge has not been removed
            //AtRow("GB").Column("Insurance charge").Expect("10.12345%");

            AtRow("GB").Column("Edit").Click("Edit");

            WaitToSeeHeader("Ancillary Details");
            Set("Freight charge per tonne").To("200");
            Set("Full load freight charge").To("800");
            Click("Cancel");

            ExpectRow("GB");
            AtRow("GB").Column("Freight charge per tonne").Expect("£120.00");
            AtRow("GB").Column("Full load freight charge").Expect("£100.00");
            AtRow("GB").Column("Value for VAT").Expect("£50.00");
            //Insurance charge has not been implemented yet
            //AtRow("GB").Column("Insurance charge").Expect("10.12345%");
        }
    }
}