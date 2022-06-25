using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GVMSOnlyUserAssertsGVMSPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyUserForGVMSOnly>();
            LoginAs<GvmsUser_GvmsOnly>();

            //As user has 'Settings' Permissions, expect Settings page
            ExpectHeader("Record Details");

            //Assert GVMS page link
            ExpectLink("GVMS");
        }
    }
}