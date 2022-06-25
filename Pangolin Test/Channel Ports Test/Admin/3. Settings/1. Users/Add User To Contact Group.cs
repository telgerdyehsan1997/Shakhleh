using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddUserToContactGroup : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD";
            var contactUser = "JOHN";
            var contactGroup = "New Group";


            Run<CreateNewCompanyUser_JohnSmith, NewContactGroup_Truckers>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Companies");
            ExpectHeader("Companies");
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Contact Groups");
            ExpectHeader("Contact Groups");
            AtRow(contactGroup).Column("Contacts").ClickLink();
            AtRow(contactUser).ClickCheckbox();
            ClickButton("Save");
            AtRow(contactGroup).Column("Contacts").Expect("1");
        }
    }
}