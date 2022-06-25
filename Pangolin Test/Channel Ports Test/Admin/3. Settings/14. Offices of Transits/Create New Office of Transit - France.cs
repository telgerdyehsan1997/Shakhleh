using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewOfficeOfTransit_France : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Seetings
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Offices of Transit");
            ExpectHeader("Offices of Transit");

            //Create new Office of Transit
            ClickLink("New Office of Transit");
            ExpectHeader("Office of Transit details");
            Set("Country code").To("FR");
            Set("Country name").To("France");
            Set("NCTS Code").To("FRCALTRA");
            Set("Usual name").To("FRANCE TRANSIT");
            AtLabel("Destination").ClickLabel("Yes");
            AtLabel("Departure").ClickLabel("Yes");
            AtLabel("Destination").ClickLabel("Yes");
            AtLabel("Transit").ClickLabel("Yes");
            ClickButton("Save");

            //Assert that details have been saved
            ExpectHeader("Offices of Transit");
            AtRow("FRANCE").Column("Country name").Expect("FRANCE");
        }
    }
}