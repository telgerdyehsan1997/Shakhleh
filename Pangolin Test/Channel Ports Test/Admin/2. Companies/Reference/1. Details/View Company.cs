using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewCompany : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //run test to add New Company
            Run<AdminAddsCompanyWorldwideLogisticsLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            WaitToSeeHeader("Shipments");
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //check details are saved in Companies > [Company Name]
            AtRow(That.Contains, "Worldwide Logistics Ltd").Column(That.Contains, "Company name").ClickLink();
            WaitToSeeHeader("Worldwide Logistics Ltd");
            Near("Type").Expect("Customer");
            Near("Customer account number").Expect("A1235");
            Near("Country").Expect("France");
            Near("Postcode/Zip code").Expect("WR5 3DA");
            Near("Address line 1").Expect("Lock Keepers Cottage");
            Near("Address line 2").Expect("Basin Road");
            Near("Town/city").Expect("Worcester");
            Near("EORI number").Expect("GB683470514001");
            Near("Branch identifier").Expect("BR123");
            Near("AEO number").Expect("IEAEOC11223344556677");
            Near("CFSP").Expect("None");
            Near("Default declarant").Expect("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            Near("Payment type").Expect("CODE A");
            Near("Deferment number").Expect("1234567");
            Near("Representation type").Expect("Direct");
            Near("Transit Guarantee").Expect("12345");
            Near("TIN").Expect("BR012345678910");
            Near("PIN").Expect("BR987654321012");
        }
    }
}