using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCompanyUser_JohnSmith : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCompanyUser_JohnSmith>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Click("Truckers Ltd");
            Expect("Company Users");
            Click("Company Users");
            WaitToSeeHeader("Company Users");

            // ----------------------------------------------

            // Edit contact - cancel
            AtRow("John").Column("Edit").Click("Edit");
            WaitToSeeHeader("Company User Details");
            Set("First name").To("Edeet");
            Set("Last name").To("Ted");
            Set("Email address").To("eted@uat.co");
            Set("Telephone number").To("23456789098");
            Set("Mobile number").To("07923987928");
            Set("Notes").To("Edited contact");
            Click("Cancel");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader("Company Users");
            ExpectNoRow("Edeet");

            // ----------------------------------------------

            // Edit contact - save
            AtRow("John").Column("Edit").Click("Edit");
            WaitToSeeHeader("Company User Details");
            Set("First name").To("Edeet");
            Set("Last name").To("Ted");
            Set("Email address").To("eted@uat.co");
            Set("Telephone number").To("23456789098");
            Set("Mobile number").To("07923987928");
            Set("Notes").To("Edited contact");
            Click("Save");

            // ----------------------------------------------

            // Expect
            WaitToSeeHeader("Company Users");
            ExpectRow("Edeet");
            AtRow("Edeet").Column("Last name").Expect("Ted");
            AtRow("Edeet").Column("Email address").Expect("eted@uat.co");
            AtRow("Edeet").Column("Telephone number").Expect("23456789098");
            AtRow("Edeet").Column("Mobile number").Expect("07923987928");
        }
    }
}