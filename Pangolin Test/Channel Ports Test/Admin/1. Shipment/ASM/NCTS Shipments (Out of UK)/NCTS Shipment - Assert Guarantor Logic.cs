using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSShipment_AssertGuarantorLogic : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var firstCompany = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";
            var secondCompany = "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2, AdminAddsCompanyTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments
            this.NavigateToNCTSShipments();
            ClickLink("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");
            ClickHeader("Shipment details");

            //Checks Guarantor for first Company
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, firstCompany);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, firstCompany);
            AtLabel("Guarantor").Expect("MAJIMA CONSTRUCTION");

            //Checks Guarantor for second Company
            Set("Company name").To("");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, secondCompany);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, secondCompany);
            AtLabel("Guarantor").Expect("TRUCKERS LTD");
        }
    }
}