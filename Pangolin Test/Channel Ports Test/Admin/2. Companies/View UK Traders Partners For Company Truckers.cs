using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewUKTradersPartnersForCompanyTruckers : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentsForTruckersLimitedGBP>();
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            //WaitToSeeHeader("Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            AtRow(That.Contains, "Truckers").Column("Company name").ClickLink();
            ExpectHeader(That.Contains, "Truckers");

            Click("UK Traders/ Partners");
            ExpectHeader("UK Traders/ Partners");



            ExpectNoRow(That.Contains, "TRUCKERS");

            AtRow("Partner").Column("Name").Expect("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
        }
    }
}