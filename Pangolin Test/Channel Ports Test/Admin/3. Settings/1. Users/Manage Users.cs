using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageUsers : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<EditUser_Alice,ArchiveUserNorman>();
            // ----------------------------------------------

            //login as admin
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            ExpectRow("Kelly");
            ExpectNoRow("Norman");

            // ----------------------------------------------

            //assert layout
            RightOfHeader("Users").Expect("New User");
            BelowHeader(That.Contains, "Users").Expect(What.Contains, "Find");
            Below("Find").Expect("Status");
            Below("New User").RightOf(What.Contains, "Find").Expect("Search");

            // ----------------------------------------------

            //create new, assert new window layout, and cancel
            Click("New User");
            WaitToSeeHeader(That.Contains, "User Details");
            BelowHeader(That.Contains, "User Details").ExpectLabel(That.Contains, "First name");
            BelowLabel(That.Contains, "First name").ExpectLabel(That.Contains, "Last name");
            BelowLabel(That.Contains, "Last name").ExpectLabel(That.Contains, "Email");
            BelowLabel(That.Contains, "Email").ExpectLabel(That.Contains, "Admin");
            NearLabel(That.Contains, "Admin").ExpectButton(That.Contains, "Cancel");
            NearLabel(That.Contains, "Admin").ExpectButton(That.Contains, "Save");

            //check Mandatory fields behave as expected
            Click("Save");
            WaitToSee(What.Contains, "The First name field is required.");
            WaitToSee(What.Contains, "The Last name field is required.");
            WaitToSee(What.Contains, "The Email field is required.");

            Set("First name").To("Joe");
            Set("Last name").To("Bloggs");
            Set("Email").To("jbloggs@uat.co");
            ClickLabel("Admin");
            Click("Cancel");

            // ----------------------------------------------

            //assert details are not saved in the list
            WaitToSeeHeader(That.Contains, "Users");
            ExpectNoRow(That.Contains, "Bloggs");
            ExpectNoRow(That.Contains, "jbloggs@uat");

            // ----------------------------------------------

            //create new user, check Mandatory fields behave as expected
            Click("New User");
            WaitToSeeHeader(That.Contains, "User Details");
            Click("Save");
            WaitToSee(What.Contains, "The First name field is required.");
            WaitToSee(What.Contains, "The Last name field is required.");
            WaitToSee(What.Contains, "The Email field is required.");
            Click("Cancel");

            // ----------------------------------------------

            //asert details in list
            WaitToSeeHeader(That.Contains, "Users");
            AtRow(That.Contains, "Green").Column(That.Contains, "First name").Expect(What.Contains, "Kelly");
            AtRow(That.Contains, "Green").Column(That.Contains, "Last name").Expect(What.Contains, "Green");
            AtRow(That.Contains, "Green").Column(That.Contains, "Email").Expect(What.Contains, "kelly@uat.co");
            AtRow(That.Contains, "Green").Column(That.Contains, "Admin").ExpectTick();

            // ----------------------------------------------

            // test search - exclusive
            Set("Find").To("Nothing");
            ClickLabel("All");
            Click("Search");
            ExpectNoRow("Norman");
            ExpectNoRow("Kelly");

            // ----------------------------------------------

            // test search - inclusive
            Set("Find").To("Norman Freeman");
            Click("Search");
            WaitToSeeHeader(That.Contains, "Users");
            ExpectRow(That.Contains, "Freeman");
            AtRow(The.Top, That.Contains, "Freeman").Column(That.Contains, "First name").Expect(What.Contains, "Norman");
            AtRow(The.Top, That.Contains, "Freeman").Column(That.Contains, "Last name").Expect(What.Contains, "Freeman");
            AtRow(The.Top, That.Contains, "Freeman").Column(That.Contains, "Email").Expect(What.Contains, "nfreeman@uat.co");
            AtRow(The.Top, That.Contains, "Freeman").Column(That.Contains, "Admin").ExpectNoTick();
            ExpectNoRow(That.Contains, "Kelly");

            // ----------------------------------------------

            // test search - active
            Set("Find").To("");
            ClickLabel("Active");
            Click("Search");
            ExpectRow("Kelly");
            ExpectNoRow("Norman");

            // ----------------------------------------------

            // test search - archived
            ClickLabel("Archived");
            Click("Search");
            ExpectNoRow("Kelly");
            ExpectRow("Norman");

            // ----------------------------------------------

            // test search - archived
            ClickLabel("All");
            Click("Search");
            ExpectRow("Kelly");
            ExpectRow("Norman");

            // ----------------------------------------------

            //Column header sorting
            /*Click("First name");
            BelowRow("Alice").Expect("Geeks");
            BelowRow("Geeks").Expect("Jack");
            BelowRow("Jack").Expect("Norman");

            Click("First name");
            BelowRow("Norman").Expect("Jack");
            BelowRow("Jack").Expect("Geeks");
            BelowRow("Geeks").Expect("Alice");

            Click("Last name");
            BelowRow("Admin").Expect("Freeman");
            BelowRow("Freeman").Expect("Smith");
            BelowRow("Smith").Expect("Spat");

            Click("Last name");
            BelowRow("Spat").Expect("Smith");
            BelowRow("Smith").Expect("Freeman");
            BelowRow("Freeman").Expect("Admin");

            Click("Email");
            BelowRow("admin@uat.co").Expect("aspat@uat.co");
            BelowRow("aspat@uat.co").Expect("nfreeman@uat.co");
            BelowRow("nfreeman@uat.co").Expect("JackSmith@uat.co");

            Click("Email");
            BelowRow("JackSmith@uat.co").Expect("nfreeman@uat.co");
            BelowRow("nfreeman@uat.co").Expect("aspat@uat.co");
            BelowRow("aspat@uat.co").Expect("admin@uat.co");

            Click(The.Top, "Admin");
            BelowRow("Jack").Expect("Freeman");
            BelowRow("Freeman").Expect("Alice");

            Click(The.Top, "Admin");
            BelowRow("Alice").Expect("Geeks");
            BelowRow("Geeks").Expect("Norman");*/
        }
    }
}
