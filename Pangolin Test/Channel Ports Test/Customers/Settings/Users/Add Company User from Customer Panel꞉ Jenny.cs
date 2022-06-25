using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyUserFromCustomerPanelJenny : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithCreatesACustomerAccount>();

            // ----------------------------------------------

            LoginAs<JohnSmithCustomer>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            Click("Users");
            WaitToSeeHeader("Company Users");
            Click("New Company User");

            // ----------------------------------------------

            // Create new company user
            Set("First name").To("Jenny");
            Set("Last name").To("Smith");
            Set("email address").To("jennysmith@uat.co");
            Set("Telephone number").To("12345678909");
            Set("Mobile number").To("07986543214");
            AtLabel("Customer Admin").ClickLabel("Yes");
            Click("Save");

            // ----------------------------------------------

            // Expect the correct details
            ExpectRow("Jenny");
            AtRow("Jenny").Column("Customer admin").ExpectTick();
            AtRow("Jenny").Column("Last name").Expect("Smith");
            AtRow("Jenny").Column("Email address").Expect("jennysmith@uat.co");
            AtRow("Jenny").Column("Telephone number").Expect("12345678909");
            AtRow("Jenny").Column("Mobile number").Expect("07986543214");
        }
    }
}