using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsNewChargeToCompany : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddDefaultLicenseBasic>();

            LoginAs<ChannelPortsAdmin>();
            AssumeDate("01/04/2021");
            Goto("/");

            ExpectHeader("Shipments");
            Click("Companies");
            Click("Imports Ltd");

            ExpectHeader("Imports Ltd");
            ClickLink("Custom Licenses");
            ExpectHeader("Custom Licenses");
            ClickLink("New Custom Licenses");
            ExpectHeader("Custom License Fee Details");

            //---checks validation from 1st of the month
            Set("Name").To("BASIC CUSTOM");
            Set("Default License").To("BASIC");
            Set("Valid from").To("03/03/2021");
            Click("Save");

            Expect("Valid From must start from first day of the month.");
            Click("OK");

            Set("Valid from").To("01/04/2021");
            Click("Save");

            ExpectRow("BASIC CUSTOM");


        }
    }
}