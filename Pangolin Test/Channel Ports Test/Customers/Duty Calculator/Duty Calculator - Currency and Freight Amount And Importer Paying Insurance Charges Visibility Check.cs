using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DutyCalculatorCurrencyAndFreightAmountAndImporterPayingInsuranceChargesVisibilityCheck : UITest
    {
        [TestProperty("Sprint", "30")]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddCompanyUserForWWLRichardSmith, CreateNewTermsOfSale_EXO_DDP>();
            LoginAs<RichardSmithCustomer>();

            //Navigation
            Click("Duty Calculator");

            ExpectNoField("Is importer paying insurance charges?");
            ExpectNoField("Insurance Amount Currency");
            ExpectNoField("Insurance Amount");

            //Filling Form
            Set("Total Value Currency").To("EUR");
            Set("Total Value").To("100");
            Set("Terms of Sale").To("EXO - EXO LTD");
            System.Threading.Thread.Sleep(1000);

            Expect("Is importer paying insurance charges?");
            ExpectField("Total Freight Amount Currency");
            ExpectField("Total Freight Amount");
            ExpectLabel("DDP Options");
        }
    }
}