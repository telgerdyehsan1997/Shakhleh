using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCheckChargePageDetails : UITest
    {
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            //Test Case has been ignored as Charges workflow no longer exists
            Run<CreateNewAdminUser_AliceSpat>();
            LoginAs<AliceSpatAdmin>();

            ExpectHeader("Shipments");

            Click("Companies");
            Click("Channel Ports");
            ExpectHeader("Channel Ports");
            Click("Charges");
            ExpectHeader("Charges");

            Click("New Charge");
            ExpectHeader("Charge Details");

            //---checking if all field are there compare to wireframe

            ExpectField("Valid from");
            ExpectField("Default License");
            ExpectField("License Fee");
            ExpectField("Free Consignments (Monthly)");
            ExpectField("Free Consignments (Yearly)");
            ExpectField("Price Per Additional Consignment");
            ExpectField("Price Per Commodity");


        }
    }
}