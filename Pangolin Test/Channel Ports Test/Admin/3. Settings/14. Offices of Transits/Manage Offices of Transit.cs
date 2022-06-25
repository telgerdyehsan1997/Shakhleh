using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageTransitOffices : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewTransitOffice>();

            // ------------------------------
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            ExpectHeader("Users");

            Click("Offices of Transit");
            ExpectHeader("Offices of Transit");

            // Add - Cancel
            Click("New Office of Transit");
            WaitToSeeHeader("Office of Transit details");
            Click("Cancel");

            // Edit - Cancel
            AtRow("Dover Office2").Click("Edit");
            WaitToSeeHeader("Office of Transit details");
            Set("Usual name").To("Office Dover");
            Set("Alias").To("Dove");
            Set("NCTS Code").To("DO123456");
            AtLabel("Destination").ClickLabel("Yes");
            Click("Cancel");


            // Edit - Save
            AtRow("Dover Office2").Click("Edit");
            WaitToSeeHeader("Office of Transit details");
            Set("Usual name").To("Dover Transit Office");
            Set("Alias").To("Dover Transit");
            Set("NCTS Code").To("DO987654");
            AtLabel("Destination").ClickLabel("Yes");
            Click("Save");

            ExpectNoRow("Dover Office2");
            ExpectRow("Dover Transit Office");

            // Archive - Cancel
            AtRow("Dover Transit Office").Click("Archive");
            ExpectHeader("Archive");
            Expect("Please Explain Why");
            Click("Cancel");
            AtRow("Dover Transit Office").Column("Archive").Expect("Archive");

            // Archive - OK
            AtRow("Dover Transit Office").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive reason");
            ClickButton("Archive");
            ExpectNoRow("Dover Transit Office");
            ClickLabel("All");
            Click("Search");
            ExpectRow("Dover Transit Office");
            AtRow("Dover Transit Office").Column("Archive").Expect("Unarchive");

            // Find
            Set("Find").To("Dover");
            Click("Search");
            ExpectRow("Dover Transit Office");

            Set("Find").To("Ports");
            Click("Search");
            ExpectNoRow("Dover");

            // Validation
            Click("New Office of Transit");
            WaitToSeeHeader("Office of Transit details");
            Click("Save");

            Expect("The Usual Name field is required.");
            Expect("The NCTS Code field is required.");

            Set("Country code").To("CA");
            Set("Country name").To("Canada");
            Set("Usual name").To("BlackPool Transit Office");
            Click("Add Alias");
            Set("Alias").To("BlackPool");
            Click("Add Alias");
            AtRow(2).Set("Alias").To("Blackpool");
            Set("NCTS Code").To("DO987654");
            AtLabel("Departure").ClickLabel("Yes");
            AtLabel("Destination").ClickLabel("Yes");
            AtLabel("Transit").ClickLabel("Yes");

            Click("Save");

            ExpectText(That.Contains, "NCTS Code must be unique. There is an existing Offices of Transit record with the provided NCTS Code.");
            Click("OK");

            // Check one the flag
            Set("NCTS Code").To("DO987657");
            Click("Save");
        }
    }
}