using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddPaperLicence_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddLicenceStatusCode_Paper_IntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");

            ExpectHeader("Users");

            //Navigate to Licences
            ClickLink("Licences");

            ExpectHeader("Licences");

            //Creates the new Licence
            ClickLink("New Licence");

            ExpectHeader("Licence details");
            AtLabel("Type").ClickLabel("Into uk");
            Set("Licence Name").To("Cultural");
            AtLabel("Licence Type").ClickLabel("Paper");
            Set("Licence Identifier").To("P1");
            Set("Chief licence code").To("X018");
            Set("Licence status code").To("PAPER");
            AtLabel("Quantity").ClickLabel("Yes");
            AtLabel("RPTID").ClickLabel("Yes");
            ClickButton("Save");

            ExpectHeader("Licences");
            ExpectRow("Cultural");
        }
    }
}