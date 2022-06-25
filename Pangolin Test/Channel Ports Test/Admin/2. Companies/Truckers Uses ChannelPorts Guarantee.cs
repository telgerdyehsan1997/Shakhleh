using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TruckersUsesChannelPortsGuarantee : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD";

            Run<AdminAddsCompanyTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();

            AtRow(companyName).Column("Edit").ClickLink();
            ExpectHeader("Record Details");

            //Removes the Guarantor
            AtLabel("Guarantor Type").ClickLabel("None");
            ClickButton("Save");
            ExpectHeader("Companies");
        }
    }
}