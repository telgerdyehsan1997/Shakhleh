using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAuthorisedLocationsToCompanyTruckers : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd, AddGuaranteeLength10DaysForWareHouse1, AddGuaranteeLength8DaysForWareHouse1>();
            LoginAs<ChannelPortsAdmin>();


            Click("Companies");
            WaitToSeeHeader("Companies");

            AtRow(That.Contains, "Truckers").Column("Edit").Click("Edit");
            RightOf("Authorised locations").Click("Nothing selected");
            Click("Stop 24");
            Click("Warehouse 1");
            Click("2 items selected");
            Click("Save");
        }
    }
}
