using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveCompanyUserJohn : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCompanyUser_JohnSmith>();
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            ExpectRow("Truckers Ltd");
            AtRow("Truckers Ltd").Click("Truckers Ltd");
            Expect("Company Users");
            Click("Company Users");

            // Archive - cancel
            AtRow("John").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            ClickButton("Cancel");
            ExpectRow("John");

            // Archive - confirm
            AtRow("John").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectNoRow("John");

        }
    }
}