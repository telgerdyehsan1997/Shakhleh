using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCPC_12345 : UITest
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

            ClickLabel("Into UK");
            Set("CPC number").To("CP12345");
            Set("CPC description").To("This is a CPC_12345");
            Set("Box 44").To("N954 AG EUR");
            //AtLabel("Manual").Click("No");
            ClickXPath(@"//*[@id=""Manual""]/div[2]/label");
            //AtLabel("Override EORI").Click("No");
            ClickXPath(@"//*[@id=""OverrideEORI""]/div[2]/label");
            //AtLabel("No tax line").Click("No");
            ClickXPath(@"//*[@id=""NoTaxLine""]/div[2]/label");


            Click("Save");

            WaitToSeeHeader("CPC");
            ExpectRow("CP12345");
            AtRow("CP12345").Column("Box 44").Expect("N954 AG EUR");
            
        }
    }
}