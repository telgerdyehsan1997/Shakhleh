using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ShipmentPage_AssertSafetyAndSecurityFieldAppears : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2>();

            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipment
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");

            //Sets Company with Safety and Security selected
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);
            AtLabel("Type").ClickLabel("Into UK");

            //Asserts Safety and Security label appears
            ExpectLabel("Safety and security");

            //Sets Company without Safety and Security selected
            Set("Company name").To("");
            ClickHeader("Shipment Details");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            //Asserts Safety and Security label disappears
            ExpectNoLabel("Safety and security");
        }
    }
}