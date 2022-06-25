using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DutyCalculatorCommodityVATAndDutyEstimationCheck : UITest
    {
        [TestProperty("Sprint", "30")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyUserForWWLRichardSmith, CreateNewTermsOfSale_EXO_DDP>();
            LoginAs<RichardSmithCustomer>();

            //Navigation
            Click("Duty Calculator");

            //Filling Form
            //Consignment
            Set("Total Value Currency").To("EUR");
            Set("Total Value").To("100");
            Set("Terms of Sale").To("EXO - EXO LTD");
            Set("Total Freight Amount Currency").To("EUR");
            Set("Total Freight Amount").To("10");
            Set("DDP Options").To("Duty and VAT Inclusive");
            System.Threading.Thread.Sleep(1000);
            AtLabel("Is importer paying insurance charges?").ClickLabel("Yes");
            System.Threading.Thread.Sleep(1000);
            Set("Insurance Amount Currency").To("EUR");
            Set("Insurance Amount").To("10");

            //Commodity
            System.Threading.Thread.Sleep(1000);
            ClickField("Commodity code");
            //Set("Commodity code").To("12345678");
            System.Threading.Thread.Sleep(1000);
            Click("12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            ClickField("Country of Origin");
            Set("Country of Origin").To("FRANC");
            System.Threading.Thread.Sleep(1000);
            Click("FRANCE");
            System.Threading.Thread.Sleep(1000);
            Set("Commodity Value").To("10");
            Set("Preference").To("No");

            //'Estimated Duty' is calculated by taking the sum of the 'Commodity Value' and 'Freight Amount' and multiplying them by the 'WTO full rate of the Commodity'
            //(10 + 10)*8.4
            AtLabel("Estimated Duty").Expect("0.92");
            AtLabel("Freight Amount").Expect("1.0");
        }
    }
}