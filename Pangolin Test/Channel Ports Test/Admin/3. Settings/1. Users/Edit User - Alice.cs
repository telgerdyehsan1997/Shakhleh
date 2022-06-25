using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditUser_Alice : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewAdminUser_AliceSpat>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            //click Edit on User
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            AtRow(That.Contains, "aspat@uat.co").Column(That.Contains, "Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "User Details");

            // ----------------------------------------------

            //assert layout on edit screen
            BelowHeader(That.Contains, "User Details").ExpectLabel(That.Contains, "First name");
            BelowLabel(That.Contains, "First name").ExpectLabel(That.Contains, "Last name");
            BelowLabel(That.Contains, "Last name").ExpectLabel(That.Contains, "Email");
            BelowLabel(That.Contains, "Email").ExpectLabel("Admin");

            // ----------------------------------------------

            //change details - cancel
            Set("First name").To("Kelly");
            Set("Last name").To("Green");
            AtLabel("Admin").ClickLabel("Yes");
            Click("Cancel");

            // ----------------------------------------------

            //expect unchanged details still associated with unchanged email address
            WaitToSeeHeader(That.Contains, "Users");
            AtRow("aspat@uat.co").Column("First name").ExpectNo("Kelly");
            AtRow("aspat@uat.co").Column("Last name").ExpectNo("Green");
            AtRow("aspat@uat.co").Column("Email").ExpectNo("kelly@uat.co");
            AtRow("aspat@uat.co").Column("Admin").ExpectTick();
            ExpectNoRow(That.Contains, "kelly@uat.co");

            // ----------------------------------------------

            //change details - save
            AtRow(That.Contains, "aspat@uat.co").Column(That.Contains, "Edit").Click("Edit");
            Set("First name").To("Kelly");
            Set("Last name").To("Green");
            AtLabel("Admin").ClickLabel("Yes");
            Click("Save");

            // ----------------------------------------------

            //expect new details still associated with unchanged email address
            WaitToSeeHeader(That.Contains, "Users");
            AtRow(That.Contains, "aspat@uat.co").Column(That.Contains, "First name").Expect(What.Contains, "Kelly");
            AtRow(That.Contains, "aspat@uat.co").Column(That.Contains, "Last name").Expect(What.Contains, "Green");
            AtRow(That.Contains, "aspat@uat.co").Column(That.Contains, "Email").Expect(What.Contains, "aspat@uat.co");
            AtRow(That.Contains, "aspat@uat.co").Column(That.Contains, "Admin").ExpectTick();
            ExpectNoRow(That.Contains, "kelly@uat.co");

            // ----------------------------------------------

            //edit again, change email, assert new email
            AtRow(That.Contains, "aspat@uat.co").Column(That.Contains, "Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "User Details");
            Set("Email").To("kelly@uat.co");
            Click("Save");
            WaitToSeeHeader("Users");
            AtRow(That.Contains, "kelly@uat.co").Column(That.Contains, "First name").Expect(What.Contains, "Kelly");
            AtRow(That.Contains, "kelly@uat.co").Column(That.Contains, "Last name").Expect(What.Contains, "Green");
            AtRow(That.Contains, "kelly@uat.co").Column(That.Contains, "Email").Expect(What.Contains, "kelly@uat.co");
            AtRow(That.Contains, "kelly@uat.co").Column(That.Contains, "Admin").ExpectTick();

            // ----------------------------------------------

            //search old details, expect nothing
            Set("Find").To("aspat@uat.co");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Users");
            ExpectNoTable();
            //expect "search returned nothing" message

            // ----------------------------------------------

            //search new details, assert new details
            Set("Find").To("kelly@uat.co");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Users");
            AtRow(The.Top, That.Contains, "kelly@uat.co").Column(That.Contains, "First name").Expect(What.Contains, "kelly");
            AtRow(The.Top, That.Contains, "kelly@uat.co").Column(That.Contains, "Last name").Expect(What.Contains, "Green");
            AtRow(The.Top, That.Contains, "kelly@uat.co").Column(That.Contains, "Email").Expect(What.Contains, "kelly@uat.co");
            AtRow(That.Contains, "kelly@uat.co").Column(That.Contains, "Admin").ExpectTick();
        }
    }
}