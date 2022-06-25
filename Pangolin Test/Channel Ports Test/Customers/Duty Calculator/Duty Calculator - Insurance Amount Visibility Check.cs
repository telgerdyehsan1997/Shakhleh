using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DutyCalculatorInsuranceAmountVisibilityCheck : UITest
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

            ExpectField("Insurance Amount Currency");
            ExpectField("Insurance Amount");
        }
    }
}