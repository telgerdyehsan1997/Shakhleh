using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class StatusAdminUndischargeNCTS : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddUndischargedNCTS_FillingForm_Microsoft>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            AtRow(That.Contains, "CP100009902").Column("Status").Expect("Discharge");
            AtRow(That.Contains, "CP100009902").Column("Status").ClickLink();

            ExpectHeader("Status");
            ExpectTable();
            ExpectRow(That.Contains, "Stage 1");
        }
    }
}