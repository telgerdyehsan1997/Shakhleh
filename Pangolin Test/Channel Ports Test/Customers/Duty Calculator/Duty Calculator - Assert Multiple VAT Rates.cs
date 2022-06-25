using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DutyCalculator_AssertMultipleVATRates : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddProductForMajimaConstructionMultipleVATRates, AddCompanyUserToMajimaConstructionMajima>();
            LoginAs<Goro_MajimaConstruction>();

            //Navigates to Duty Calculator
            ClickLink("Duty Calculator");
            ExpectHeader("Consignment Details");
        }
    }
}