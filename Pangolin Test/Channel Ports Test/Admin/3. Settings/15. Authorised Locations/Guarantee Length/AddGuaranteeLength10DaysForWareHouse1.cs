using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddGuaranteeLength10DaysForWareHouse1 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAuthorisedLocationWarehouse1>();

            LoginAs<ChannelPortsAdmin>();

            Click("Settings");

            Click("Authorised Locations");

            ExpectHeader(That.Contains, "Authorised Locations");
         
            AtRow("Warehouse 1").Column("Guarantee length").ClickLink();

            ExpectHeader("Guarantee Length");

            Click("New Guarantee Length");

            ExpectHeader("Guarantee Length");
            Set("Valid for (days)").To("10");

            Click("Save");

            ExpectRow("10");

            Click("Settings");

            Click("Authorised Locations");

            ExpectHeader(That.Contains, "Authorised Locations");          
        }
    }
}