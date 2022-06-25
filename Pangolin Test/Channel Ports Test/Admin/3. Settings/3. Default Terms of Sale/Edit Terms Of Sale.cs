using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditTermsOfSale : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewTermsOfSale_EXO>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            Click("Default Terms of Sale");
            WaitToSeeHeader("Default Terms of Sale");
            ExpectRow("EXW");
            AtRow("EXW").Column("Edit").Click("Edit");

            // ----------------------------------------------

            // Edit - cancel
            Set("Terms of sale").To("FCA");
            Set("Description").To("Edited description");
            ClickLabel("B0000");
            ClickLabel(The.Top, "No"); //Freight Charge
            ClickLabel(The.Bottom, "No"); //Value for VAT
            Click("Cancel");

            ExpectRow("EXW");
            //ExpectNoRow("FCA");

            // ----------------------------------------------

            // Edit - save
            AtRow("EXO").Column("Edit").Click("Edit");
            Set("Terms of sale").To("FGC");
            Set("Description").To("Edited description");
            ClickLabel("B0000");
            ClickLabel(The.Top, "No"); //Freight Charge
            ClickLabel(The.Bottom, "No"); //Value for VAT
            Click("Save");

            ExpectNoRow("EXO");
            ExpectRow("FGC");
            AtRow("FGC").Column("Description").Expect("Edited description");
            AtRow("FGC").Column("Box 45").Expect("B0000");
            AtRow("FGC").Column("Freight Charge").Expect("No");
            AtRow("FGC").Column("Value for VAT").Expect("10");
        }
    }
}