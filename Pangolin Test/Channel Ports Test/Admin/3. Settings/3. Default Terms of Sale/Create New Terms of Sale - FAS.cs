using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewTermsOfSale_FAS : UITest
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
            Click("Settings");
            WaitForNewPage();
            Click("Default Terms of Sale");
            WaitToSeeHeader("Default Terms of Sale");
            Click("New Terms of Sale");
            WaitToSeeHeader("Terms of Sale Details");

            // ----------------------------------------------

            // Set details
            Set("Terms of Sale").To("FAS");
            Set("Description").To("Free Carrier");
            ClickLabel("A0000");
            //Set("Freight Charge").To("No");
            NearField("Freight charge").NearLabel("Yes").ClickLabel("No");
            //Set("Value for VAT").To("Yes");
            NearField("Value for VAT").NearLabel("No").ClickLabel("Yes");
            Click("Save");

            // ----------------------------------------------

            // Expect
            ExpectRow("FAS");
            AtRow("FAS").Column("Description").Expect(What.Contains, "Free Alongside Ship");
            AtRow("FAS").Column("Box 45").Expect(What.Contains, "A0000");
            AtRow("FAS").Column("Freight Charge").Expect(What.Contains, "No");
            AtRow("FAS").Column("Value for VAT").Expect(What.Contains, "Yes");
            */
        }
    }
}