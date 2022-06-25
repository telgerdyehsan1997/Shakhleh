using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.CSharp.RuntimeBinder;
using System.ComponentModel.Design;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminChecksIfTypeDefaultsBasedOnCompany : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminChangesTruckersLtdCompanyType>();

            LoginAs<ChannelPortsAdmin>();

            ClickLink("Companies");

            // changing to Out of uk
            AtRow("TRUCKERS LTD").Column("Edit").Click("Edit");
            ExpectHeader("Record Details");
            ClickLabel("Out of uk");
            ClickLabel("Into uk");
            
            Click("Save");

            ClickLink("Companies");
            AtRow("TRUCKERS LTD").Click("Truckers Ltd");
            Click("Company Users");
            ClickLink("New Company User");
            Set("First name").To("Raf");
            Set("Last name").To("Test");
            Set("Email address").To("raftest@uat.co");
            Click("Save");

            Click("Shipments");

            Click("New Shipment");
            ExpectHeader("Shipment Details");

            //under Type checkbox for Into UK should be checked - this means it works correctly




         
















        }
    }
}