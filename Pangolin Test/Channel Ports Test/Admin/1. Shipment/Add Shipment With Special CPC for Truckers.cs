using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [Ignore]
    [TestClass]
    public class AddShipmentWithSpecialCPC : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            //Work flow no longer exists
            Run<AddSpecialCPCsToTruckersLtd, AddNewShipmentForTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0721000001");

            AtRow("R0721000001").Column("Consignments").Click("0");
            System.Threading.Thread.Sleep(1000);

            Click("New Consignment");

            ExpectLabel("Use special CPC");

            AtLabel("Use special CPC").Check();

            AtLabel("Special CPC").Type("CP12");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);

            Click("Cancel");


        }
    }
}
