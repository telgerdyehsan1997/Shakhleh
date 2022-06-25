using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveCPC : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCPC_12345>();

            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            Click("CPC");

            // archive
            AtRow("CP12345").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Click("Cancel");

            AtRow("CP12345").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archive reason");
            ClickButton("Archive");

            NearLabel("Archived").ClickLabel("Active");
            Click("Search");
            ExpectNoRow("CP12345");
        }
    }
}