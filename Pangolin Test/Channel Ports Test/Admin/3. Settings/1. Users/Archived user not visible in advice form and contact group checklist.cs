using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchivedUserNotVisibleInAdviceFormAndContactGroupChecklist : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithCreatesACustomerAccount>();

            //check john is in advice form, both primary contact and specific contacts
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            Click("TRUCKERS LTD");
            Click("Contact Groups");
            Click("New Contact Group");
            WaitToSeeHeader("Contact Group Details");
            Set("Group name").To("Managers");
            Click("Save");
            WaitToSeeHeader("Contact Groups");
            AtRow(That.Contains, "Managers").Column("Contacts").ClickLink("0");
            WaitToSeeHeader(That.Contains, "Contacts");
            ExpectRow(That.Contains, "John");
            NearText("JOHN").ClickCheckbox();
            Click("Save");

            //archive john
            Click(The.Left, "Company Users");
            WaitToSeeHeader("Company Users");
            AtRow(That.Contains, "John").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectText("There are no company users to display.");
            ClickLink("Contact Groups");
            AtRow(That.Contains, "Managers").Column("Contacts").ClickLink("0");
            WaitToSeeHeader(That.Contains, "Contacts");
            ExpectNoRow(That.Contains, "John");
            Expect("There are no people to display.");
        }
    }
}