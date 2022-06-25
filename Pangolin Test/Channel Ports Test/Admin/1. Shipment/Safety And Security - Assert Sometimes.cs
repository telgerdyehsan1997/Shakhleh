using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SafetyAndSecurity_AssertSometimes : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Majima Construction";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Companies to change Safety and Security to Sometimes
            ClickLink("Companies");
            ExpectHeader("Companies");

            AtRow(companyName).Column("Edit").ClickLink();
            AtLabel("Safety and security inbound").ClickLabel("Sometimes");
            AtLabel("Safety and security outbound").ClickLabel("Sometimes");
            ClickButton("Save");

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
            AtLabel("Safety and security").ExpectLabel("Yes");
            AtLabel("Safety and security").ExpectLabel("No");

            AtLabel("Type").ClickLabel("Out of UK");
            ExpectLabel("Safety and security");
            AtLabel("Safety and security").ExpectLabel("Yes");
            AtLabel("Safety and security").ExpectLabel("No");
        }
    }
}