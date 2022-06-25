﻿using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetAncillariesForUK : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //Run<CreateNewCountry_UnitedKingdom>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            ClickLink("Settings");
            WaitForNewPage();
            Expect("Ancillaries");
            Click("Ancillaries");
            WaitToSeeHeader("Ancillaries");
            ExpectRow("GB");

            // ----------------------------------------------

            // NOTE: To confirm default data
            AtRow("GB").Column("Freight charge per tonne").ExpectText("");
            AtRow("GB").Column("Full load freight charge").ExpectText("");
            AtRow("GB").Column("Value for VAT").ExpectText("");
            //AtRow("GB").Column("Insurance charge").ExpectText("");

            // ----------------------------------------------

            // Edit - Save
            AtRow("GB").Column("Edit").Click("Edit");
            WaitToSeeHeader("Freight Charge Details");
            Set("Freight charge per tonne").To("100");
            Set("Full load freight charge").To("150");
            Set("Value for VAT").To("50");
            //Set("Insurance charge").To("10.12345");
            Click("Save");

            // ----------------------------------------------

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