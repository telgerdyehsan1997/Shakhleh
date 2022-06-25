using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewTermsOfSale_FCA : UITest
    {
        [TestProperty("Sprint", "1")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            //This entity is now created within ReferenceData.cs
            //So running this test would throw 'Term of Sale already exists' message

            /*
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Click("Default Terms of Sale");
            WaitToSeeHeader("Default Terms of Sale");
            Click("New Terms of Sale");
            WaitToSeeHeader("Terms of Sale Details");

            // ----------------------------------------------

            // Set details
            Set("Terms of Sale").To("FCA");
            Set("Description").To("Free Carrier");
            ClickLabel("B0000");
            //Set("Freight Charge").To("Yes");
            NearField("Freight charge").NearLabel("No").ClickLabel("Yes");
            //Set("Value for VAT").To("No");
            NearField("Value for VAT").NearLabel("Yes").ClickLabel("No");
            Click("Save");

            // ----------------------------------------------

            // Expect
            ExpectRow("FCA");
            AtRow("FCA").Column("Description").Expect(What.Contains, "Free Carrier");
            AtRow("FCA").Column("Box 45").Expect(What.Contains, "B0000");
            AtRow("FCA").Column("Freight Charge").Expect(What.Contains, "Yes");
            AtRow("FCA").Column("Value for VAT").Expect(What.Contains, "No");
            */
        }
    }
}