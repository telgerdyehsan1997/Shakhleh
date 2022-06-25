using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCPC_54321 : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");

            Click("CPC");
            WaitToSeeHeader("CPC");

            Click("New CPC");
            WaitToSeeHeader("CPC Details");
            ClickLabel("Out of uk");
            Set("CPC number").To("CP54321");
            Set("CPC description").To("This is CPC_54321");
            Set("Box 44").To("N954 AG EUR");
            //AtLabel("Manual").Click("No");
            ClickXPath(@"//*[@id=""Manual""]/div[2]/label");
            //AtLabel("Override EORI").Click("No");
            ClickXPath(@"//*[@id=""OverrideEORI""]/div[2]/label");
            //AtLabel("No tax line").Click("No");
            ClickXPath(@"//*[@id=""NoTaxLine""]/div[2]/label");

            Click("Save");

            WaitToSeeHeader("CPC");
            ExpectRow("CP54321");
            AtRow("CP54321").Column("Box 44").Expect("N954 AG EUR");
           
        }
    }
}