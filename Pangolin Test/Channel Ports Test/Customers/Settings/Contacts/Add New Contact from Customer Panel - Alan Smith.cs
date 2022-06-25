using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewContactFromCustomerPanel_AlanSmith : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithCreatesACustomerAccount>();

            // ----------------------------------------------

            LoginAs<JohnSmithCustomer>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            Click("Contacts");
            WaitToSeeHeader("Contacts");
            Click("New Contact");

            // ----------------------------------------------

            // Create new company user
            Set("First name").To("Alan");
            Set("Last name").To("Smith");
            Set("Email address").To("alansmith@uat.co");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            Click("Save");

            // ----------------------------------------------

            // Expect the correct details
            ExpectRow("Alan");
            AtRow("Alan").Column("Last name").Expect("Smith");
            AtRow("Alan").Column("Email address").Expect("alansmith@uat.co");
            AtRow("Alan").Column("Telephone number").Expect("12345678909");
            AtRow("Alan").Column("Mobile number").Expect("07986543214");
        }
    }
}