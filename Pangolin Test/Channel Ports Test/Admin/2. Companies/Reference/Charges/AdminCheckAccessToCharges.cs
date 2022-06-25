using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCheckAccessToCharges : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewAdminUser_AliceSpat>();

            LoginAs<AliceSpatAdmin>();

            ExpectHeader("Shipments");

            Click("Companies");
            ExpectHeader("Companies");

            AtRow("Imports Ltd").Column("Company name").Click("Imports Ltd");
            ExpectLink("Custom Licenses");
            ClickLink("Custom Licenses");
            ExpectHeader("Custom Licenses");

            //---check details of Charge Details page

            ClickLink("New Custom Licenses");

            ExpectLabel("Valid from");
            ExpectLabel("Default License");
            ExpectLabel("Currency");
            ExpectLabel("Is Charged Yearly");
            ExpectLabel("License Fee");
            ExpectLabel("Free Consignments");
            ExpectLabel("Price Per Additional Consignment");
            ExpectLabel("Price Per Commodity");

            Click("Save");
            System.Threading.Thread.Sleep(1000);
            Click("Cancel");

            ExpectHeader("Custom Licenses");


        }
    }
}