using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveContact_CheckContactGroupValue : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD";
            var contactName = "ALAN";
            var contactGroup = "New Group";

            Run<Truckers_AddContactToContactGroup, ArchiveContact_AlanSmith>();
            LoginAs<ChannelPortsAdmin>();

            //Navigatesto Companies
            ClickLink("Companies");
            ExpectHeader("Companies");
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Contact Groups");
            ExpectHeader("Contact Groups");

            //Asserts that Archived Contact should not be Counted
            AtRow(contactGroup).Column("Contacts").Expect("0");
            AtRow(contactGroup).Column("Contacts").ClickLink();
            Expect(What.Contains, "There are no people to display.");

            //Asserts that Archived Contact should not appear
            ExpectNoRow(contactName);
            ClickButton("Save");
            ExpectHeader("Contact Groups");
        }
    }
}