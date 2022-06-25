using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AncillariesNotVisibleToNon_Admin : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "124703")]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<EditCompanyAncillaries_TruckersLtd,CreateNewNon_AdminUser_NormanFreeman>();
            LoginAs<NormanNon_Admin>();

            Click("Companies");
            Click("Truckers Ltd");
            ExpectNo("Ancillaries");
        }
    }
}
