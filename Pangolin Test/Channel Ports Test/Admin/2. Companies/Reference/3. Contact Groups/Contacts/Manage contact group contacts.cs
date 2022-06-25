using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageContactGroupContacts : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddContactsToImport>();
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Column("Company name").ClickLink();
            WaitToSeeHeader("Truckers Ltd");
            Click("Contact Groups");
            WaitToSeeHeader("Contact Groups");
            ExpectRow("Import");
            AtRow("Import").Column("Contacts").ClickLink();

            // search
            Set("Find").To("Nobody");
            Click("Search");
            ExpectNoRow("Smith");

            Set("Find").To("Smith");
            Click("Search");
            ExpectRow("Smith");
        }
    }
}