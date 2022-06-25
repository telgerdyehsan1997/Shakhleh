using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewShipment_AssertRouteFieldWhenNCTSCompanySelected : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";
            var anotherCompany = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";

            Run<AddRouteBlackpoolAndCalais, AddCompanyMajimaConstruction_DefNumberStartsWith2>();

            LoginAs<ChannelPortsAdmin>();

            //Navigate to Shipment page
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");

            //Selects a Company that uses NCTS
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            //Expects the Route field as NCTS company is selected
            ExpectField("Route");

            //Changes the Company and asserts that Route Disappears
            Set("Company name").To("");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, anotherCompany);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, anotherCompany);

            ExpectField("Route");
        }
    }
}