using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertGuaranteeLengthFieldAppearsWhenMultipleSelected : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";

            Run<AddAnAuthorisedLocationToMajimaConstruction>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipments
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");
            ClickHeader("Shipment Details");

            //Selects the Company with NCTS Enabled
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(companyName);
            System.Threading.Thread.Sleep(1000);
            Click(companyName);

            //Sets the Shipment to Out of The UK
            AtLabel("Type").ClickLabel("Out of UK");
            ExpectLabel("NCTS");
            AtLabel("NCTS").Expect("Yes");

            //Enables Authorised Location to check if Guarantee length is selectable
            ExpectLabel("Use authorised location?");
            AtLabel("Use authorised location?").ClickLabel("Yes");
            ExpectLabel("Authorised location");
            AtLabel("Authorised location").Expect("WAREHOUSE 1");
        }
    }
}