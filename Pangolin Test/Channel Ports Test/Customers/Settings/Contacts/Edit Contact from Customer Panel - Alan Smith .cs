using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditContactFromCustomerPanel_AlanSmith : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewContactFromCustomerPanel_AlanSmith>();

            // ----------------------------------------------

            LoginAs<JohnSmithCustomer>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            Click("Contacts");
            WaitToSeeHeader("Contacts");
            AtRow(That.Contains, "alansmith@uat.co").Click("Edit");

            // ----------------------------------------------

            // Create new company user
            Set("First name").To("Alan2");
            Set("Last name").To("Smith2");
            Set("Email address").To("alansmith2@uat.co");
            Set("Telephone number").To("12345678902");
            Set("Mobile number").To("07986543212");
            Click("Save");

            // ----------------------------------------------

            // Expect the correct details
            ExpectRow("Alan");
            AtRow("Alan").Column("Last name").Expect("Smith2");
            AtRow("Alan").Column("Email address").Expect("alansmith2@uat.co");
            AtRow("Alan").Column("Telephone number").Expect("12345678902");
            AtRow("Alan").Column("Mobile number").Expect("07986543212");
        }
    }
}