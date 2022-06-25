using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AliceAdminAccessingAccountingLinks : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewAdminUser_AliceSpat>();
            LoginAs<AliceSpatAdmin>();

            ExpectHeader("Shipments");
            Click("Settings");
            ExpectNoLink(That.Contains, "VAT");

            ClickLink("Accounting");
            ExpectHeader("VAT Rates");

        }
    }
}