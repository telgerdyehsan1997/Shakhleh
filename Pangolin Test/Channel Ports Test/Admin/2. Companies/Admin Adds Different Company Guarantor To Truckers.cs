using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsDifferentCompanyGuarantorToTruckers : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD";
            var guarantorName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2, AdminAddsCompanyTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Company
            this.NavigateToCompanies();
            AtRow(companyName).Column("Edit").Click("Edit");
            ExpectHeader("Record Details");

            //Chooses another Company to be the Guarantor
            AtLabel("Guarantor Type").ClickLabel("Different Company's Guarantee");
            ExpectLabel("Guarantor Name");
            ClickLabel("Guarantor Name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, guarantorName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, guarantorName);
            ClickButton("Save");
        }
    }
}