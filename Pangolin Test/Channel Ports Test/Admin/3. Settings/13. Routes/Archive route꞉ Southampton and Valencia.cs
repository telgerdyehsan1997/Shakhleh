using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveRouteSouthamptonAndValencia : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddRouteSouthamptonAndValencia>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader(That.Contains, "Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader(That.Contains, "Routes");

            AtRow("Southampton").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton(The.Left, "Archive");

            ExpectNo("Southampton");
            ExpectNo("Valencia");

            ClickLabel("Archived");
            Click("Search");

            Expect("Southampton");
            Expect("Valencia");
        }
    }
}