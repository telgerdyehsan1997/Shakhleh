using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSShipment_AssertOtherCompanyIsGuarantor : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var firstCompany = "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321";
            var secondCompany = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";

            Run<AdminAddsDifferentCompanyGuarantorToTruckers>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments
            this.NavigateToNCTSShipments();
            ClickLink("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");
            ClickHeader("Shipment details");

            //Checks Guarantor for first Company is the other Company
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, firstCompany);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, firstCompany);
            AtLabel("Guarantor").Expect("MAJIMA CONSTRUCTION");

            //Checks Guarantor for the other Company to see if it matches the first
            Set("Company name").To("");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, secondCompany);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, secondCompany);
            AtLabel("Guarantor").Expect("MAJIMA CONSTRUCTION");
        }
    }
}