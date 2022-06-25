using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveContact_AlanSmith : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD";
            var contactName = "ALAN";

            Run<AddNewContactForTruckers_AlanSmith>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            ClickLink("Companies");
            ExpectHeader("Companies");
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Contacts");

            //Archives the Contact
            ExpectHeader("Contacts");
            AtRow(contactName).Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");
            ExpectNoRow(contactName);

            //Asserts that contact is Archived
            AtLabel("Status").ClickLabel("Archived");
            ClickButton("Search");
            ExpectRow(contactName);
        }
    }
}