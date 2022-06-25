using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditExchequerCode666554 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            LoginAs<ChannelPortsAdmin>();
            Click("Accounting");
            Click("Exchequer Codes");
            ExpectHeader("Exchequer Codes");

            ExpectRow("510107");
            ExpectNo("Add New Exchequer Code");
            AtRow("510107").Click("Edit");
            ExpectHeader("Exchequer Code Details");
            Set("Nominal Code").To("666554");
            Click("Save");

            ExpectNoRow("510107");
            ExpectRow("666554");

        }
    }
}