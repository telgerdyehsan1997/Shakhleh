using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddLicence_RPTIDLicence_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddLicenceStatusCode_IntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");
            ExpectHeader("Users");

            //Navigates to Licences
            ClickLink("Licences");
            ExpectHeader("Licences");

            //Creates the new Licence
            ClickLink("New Licence");
            ExpectHeader("Licence details");
            AtLabel("Type").ClickLabel("Into uk");
            Set("Licence Name").To("RPTID");
            AtLabel("Licence Type").ClickLabel("Electronic");
            Set("Licence Identifier").To("P1");
            Set("Chief licence code").To("L001");
            Set("Licence status code").To("INTO");
            AtLabel("Quantity").ClickLabel("Yes");
            AtLabel("RPTID").ClickLabel("Yes");
            ClickButton("Save");

            //Asserts that the licence has been created
            ExpectHeader("Licences");
            ExpectRow("RPTID");
        }
    }
}