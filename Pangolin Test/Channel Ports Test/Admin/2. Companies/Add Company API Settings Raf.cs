using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyAPISettingsRaf : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewContactForGeeksQA_TestContact>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            Click("GeeksQA");
            Click("API Settings");

            // ----------------------------------------------

            //edit details
            Set("Username").To("GEEKSAPI@UAT.CO");
            Set("Password").To("GEEKS");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Test QA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Test QA");
            Click("Save");
        }
    }
}