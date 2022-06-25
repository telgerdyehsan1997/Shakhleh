using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Truckers_AddContactToContactGroup : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD";
            var contactName = "ALAN";
            var contactGroup = "New Group";


            Run<AddNewContactForTruckers_AlanSmith, NewContactGroup_Truckers>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Companies");
            ExpectHeader("Companies");
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Contact Groups");
            ExpectHeader("Contact Groups");
            AtRow(contactGroup).Column("Contacts").ClickLink();
            AtRow(contactName).ClickCheckbox();
            ClickButton("Save");
            AtRow(contactGroup).Column("Contacts").Expect("1");
        }
    }
}