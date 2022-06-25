using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddContactsToImport : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewContactGroup_Import,CreateNewCompanyUser_JohnSmith,AddNewContactForTruckers_AlanSmith>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            WaitToSeeHeader("Companies");

            ExpectRow("Truckers Ltd");

            AtRow("Truckers Ltd").Column("Company name").ClickLink();

            WaitToSeeHeader("Truckers Ltd");
            Click("Contact Groups");
            WaitToSeeHeader("Contact Groups");

            ExpectRow("Import");
            AtRow("Import").Column("Contacts").Expect("0");
            AtRow("Import").Click("0");

            WaitToSeeHeader("Contacts");
            ExpectRow("John");
            AtRow("John").Column("Last name").Expect("Smith");

            ExpectRow("Alan");
            AtRow("Alan").Column("Last name").Expect("Smith");

            AtRow("John").ClickCheckbox();
            AtRow("Alan").ClickCheckbox();
            Click("Save");

            AtRow("Import").Column("Contacts").Expect("2");
            AtRow("Import").Column("Contacts").ClickLink();

            WaitToSeeRow("John");
            AtRow("John").Column("First name").Expect("John");
            AtRow("John").Column("Last name").Expect("Smith");
            WaitToSeeRow("Alan");
            AtRow("Alan").Column("First name").Expect("Alan");
            AtRow("Alan").Column("Last name").Expect("Smith");
        }
    }
}
