using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewContactGroup_Truckers : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD";
            var contactGroup = "New Group";

            Run<AdminAddsCompanyTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            ClickLink("Companies");
            ExpectHeader("Companies");
            AtRow(companyName).Column("Company name").ClickLink();
            ExpectHeader(companyName);
            ClickLink("Contact Groups");
            ExpectHeader("Contact Groups");
            ClickLink("New Contact Group");

            //Creates the new Contact Group
            ExpectHeader("Contact Group Details");
            Set("Group name").To(contactGroup);
            Click("Save");
            ExpectRow(contactGroup);
        }
    }
}