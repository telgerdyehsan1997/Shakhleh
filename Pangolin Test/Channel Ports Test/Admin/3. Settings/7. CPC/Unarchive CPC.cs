using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveCPC : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveCPC>();

            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            Click("CPC");

            ClickLabel("Archived");
            Click("Search");

            // archive
            AtRow("CP12345").Click("Unarchive");
            ExpectHeader("Unarchive");
            ClickButton("Cancel");

            AtRow("CP12345").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchedive Reason");
            ClickButton("Unarchive");

            ExpectNoRow("CP12345");
        }
    }
}