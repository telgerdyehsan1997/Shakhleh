using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewOfficeOfTransit_Italy : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Seetings
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Offices of Transit");
            ExpectHeader("Offices of Transit");

            //Create new Office of Transit
            ClickLink("New Office of Transit");
            ExpectHeader("Office of Transit details");
            Set("Country code").To("IT");
            Set("Country name").To("Italy");
            Set("NCTS Code").To("IT112345");
            Set("Usual name").To("IT");
            AtLabel("Destination").ClickLabel("Yes");
            AtLabel("Departure").ClickLabel("Yes");
            AtLabel("Destination").ClickLabel("Yes");
            AtLabel("Transit").ClickLabel("Yes");
            ClickButton("Save");

            //Assert that details have been saved
            ExpectHeader("Offices of Transit");
            AtRow("ITALY").Column("Country name").Expect("ITALY");
            AtRow("ITALY").Column("Country code").Expect("IT");
            AtRow("ITALY").Column("NCTS Code").Expect("IT112345");
            AtRow("ITALY").Column("Usual name").Expect("IT");
            AtRow("ITALY").Column("Departure").ExpectTick();
            AtRow("ITALY").Column("Destination").ExpectTick();
            AtRow("ITALY").Column("Transit").ExpectTick();
        }
    }
}