using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCompanyUserFromCustomerPanelJenny : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //Run<AddCompanyUserFromCustomerPanelJenny>();

            // ----------------------------------------------

            LoginAs<JohnSmithCustomer>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            Click("Users");
            WaitToSeeHeader("Company Users");
            AtRow(That.Contains, "jennysmith@uat.co	").Click("Edit");
            // ----------------------------------------------

            // Create new company user
            Set("First name").To("Jenny2");
            Set("Last name").To("Smith2");
            Set("email address").To("jennysmith2@uat.co");
            Set("Telephone number").To("12345678902");
            Set("Mobile number").To("07986543212");
            AtLabel("Customer Admin").ClickLabel("No");
            Click("Save");

            // ----------------------------------------------

            // Expect the correct details
            ExpectRow("Jenny");
            AtRow("Jenny").Column("Customer admin").ExpectNoTick();
            AtRow("Jenny").Column("Last name").Expect("Smith2");
            AtRow("Jenny").Column("Email address").Expect("jennysmith2@uat.co");
            AtRow("Jenny").Column("Telephone number").Expect("12345678902");
            AtRow("Jenny").Column("Mobile number").Expect("07986543212");
        }
    }
}