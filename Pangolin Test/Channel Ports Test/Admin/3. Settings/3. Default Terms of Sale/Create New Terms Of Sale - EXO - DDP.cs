using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewTermsOfSale_EXO_DDP : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            ClickLink("Settings");
            WaitForNewPage();
            Click("Default Terms of Sale");
            WaitToSeeHeader("Default Terms of Sale");
            Click("New Terms of Sale");
            WaitToSeeHeader("Terms of Sale Details");

            // ----------------------------------------------

            // Set details
            Set("Terms of Sale").To("EXO");
            Set("Description").To("EXO Ltd");
            ClickLabel("A0000");
            NearField("Freight charge").NearLabel("No").ClickLabel("Yes");
            NearField("Value for VAT").NearLabel("No").ClickLabel("Yes");
            Set("Value for VAT").To("10");
            AtLabel("Is DDP").ClickLabel("Yes");
            Click("Save");

            // ----------------------------------------------

            // Expect
            ExpectRow("EXO");
            AtRow("EXO").Column("Description").Expect(What.Contains, "EXO Ltd");
            AtRow("EXO").Column("Box 45").Expect(What.Contains, "A0000");
            AtRow("EXO").Column("Freight Charge").Expect(What.Contains, "Yes");
            AtRow("EXO").Column("Value for VAT").Expect("10");
        }
    }
}