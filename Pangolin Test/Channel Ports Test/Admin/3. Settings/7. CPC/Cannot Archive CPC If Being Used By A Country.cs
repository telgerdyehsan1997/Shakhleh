using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CannotArchiveCPCIfUBeingUsedByACountry : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCPC_12345, CreateNewCPC_54321, CreateNewCountry_France>();
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            WaitToSeeHeader("Users");
            Click("CPC");
            WaitToSeeHeader("CPC");

            AtRow("CP12345").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");

            Expect("Cannot archive this CPC. At least one country is using this CPC for importing/exporting.");
        }
    }
}
