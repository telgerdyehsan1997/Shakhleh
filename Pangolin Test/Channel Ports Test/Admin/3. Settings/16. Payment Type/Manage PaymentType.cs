using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManagePaymentType : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "128144")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<PaymentTypeA>();

            // ------------------------------
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");

            Click("Payment Types");

            WaitToSeeHeader("Payment Types");

            // Add - Cancel
            Click("Payment Type");
            WaitToSeeHeader("Payment Type Details");
            Click("Cancel");

            // Edit - Cancel
            AtRow("A").Click("Edit");
            WaitToSeeHeader("Payment Type Details");
            Set("Code").To("C");
            Set("Description").To("code C");
            Click("Cancel");

            ExpectNoRow("C");
            ExpectRow("A");

            // Edit - Save
            AtRow("A").Click("Edit");
            WaitToSeeHeader("Payment Type Details");
            Set("Code").To("C");
            Set("Description").To("code C");
            Click("Save");

            ExpectNoRow("A");
            ExpectRow("C");

            // Archive - Cancel
            AtRow("C").Click("Archive");
            ExpectHeader("Archive");
            Expect("Please Explain Why");
            Click("Cancel");
            AtRow("C").Column("Archive").Expect("Archive");

            // Archive - OK
            AtRow("C").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectNoRow("C");
            ClickLabel("All");
            Click("Search");
            ExpectRow("C");
            AtRow("C").Column("Archive").Expect("Unarchive");

            // Find
            Set("Find").To("C");
            Click("Search");
            ExpectRow("C");

            Set("Find").To("Ports");
            Click("Search");
            ExpectNoRow("C");
            Set("Find").To("");
            Click("Search");
            ExpectRow("C");

            // Validation
            Click("Payment Type");
            WaitToSeeHeader("Payment Type Details");
            Click("Save");

            Expect("The Code field is required.");
            Expect("The Description field is required.");

            Set("Code").To("C");
            Set("Description").To("Code C");
            Click("Save");

            Expect("Code must be unique. There is an existing Payment Type record with the provided code.");
            Click("OK");
            Set("Code").To("D");
            Click("Save");

            ExpectRow("D");
        }
    }
}