using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TransmitShipmentForTruckersLTD_EURUSDExchangeRates : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddCommoditiesForTruckersLTDUSDEUR, AdminAddsExchangeRates>();
            LoginAs<ChannelPortsAdmin>();

            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            Click("T0721000001");
            //todo finish workflow when resolved
            Click(The.Top, "Transmit");

            ExpectNo("Exchange rate service failed.");
        }
    }
}
