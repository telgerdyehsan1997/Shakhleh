using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewContactForGeeksQA_TestContact : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyGeeksQA>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("GeeksQA");
            AtRow("GeeksQA").Column("Company name").ClickLink("");
            Click("Contacts");
            ExpectHeader("Contacts");

            //Adds the new Contact
            ClickLink("New Contact");
            ExpectHeader("Contact details");
            Set("First name").To("Test");
            Set("Last name").To("QA");
            Set("Email address").To("test.qa@uat.co");
            ClickButton("Save");
            ExpectRow("Test");
        }
    }
}