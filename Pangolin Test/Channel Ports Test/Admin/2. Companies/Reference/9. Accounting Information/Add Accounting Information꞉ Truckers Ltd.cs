using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAccountingInformationTruckersLtd : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();
            Click("Companies");
            AtRow("Truckers Ltd").Column("Company name").ClickLink();
            Click("Accounting Information");

            ExpectHeader("Accounting Information");
            ExpectLabel("Send invoices to accounting department");
            AtLabel("Send invoices to Accounting Department").ClickLabel("Yes");
            Set("Department name").To("Truckers Dep");
            Set("Department email").To("TruckersDep@uat.co");
            Set("Address line 1").To("home street 45");
            Set("Address line 2").To("The tall building");
            ExpectLabel("Address line 3");
            Set("Country").To("United Kingdom");
            Set("Postcode").To("LLC 6GH");
            ExpectLabel("Placed on hold by");
            Set("Invoice frequency").To("Monthly");

            Click("Save");
        }
    }
}