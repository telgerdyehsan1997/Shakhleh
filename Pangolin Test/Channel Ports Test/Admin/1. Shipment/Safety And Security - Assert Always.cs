using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SafetyAndSecurity_AssertAlways : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Majima Construction";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipments
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");
            ClickHeader("Shipment Details");

            //Asserts that Safety and Security has a checkbox
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            AtLabel("Type").ClickLabel("Into UK");
            ExpectLabel("Safety and security");
            AtLabel("Safety and security").Expect("Yes");

            AtLabel("Type").ClickLabel("Out of UK");
            ExpectLabel("Safety and security");
            AtLabel("Safety and security").Expect("Yes");
        }
    }
}