using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DutyCalculatorPreferenceTypeVisibilityAndOptionsCheck : UITest
    {
        [TestProperty("Sprint", "30")]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddCompanyUserForWWLRichardSmith, CreateNewTermsOfSale_EXO_DDP, UploadCommodityCodeWithEuQuota>();
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
            Click("1012100 - 13");
            System.Threading.Thread.Sleep(1000);
            ClickField("Country of Origin");
            System.Threading.Thread.Sleep(1000);
            Click("FRANCE");
            Set("Commodity Value").To("10");
            AtLabel("Preference").ClickLabel("Yes");
            ExpectLabel("Preference type");

            //Estimated Duty should be 0 if Preference is set to 'Yes'
            AtLabel("Estimated Duty").Expect("0");
        }
    }
}