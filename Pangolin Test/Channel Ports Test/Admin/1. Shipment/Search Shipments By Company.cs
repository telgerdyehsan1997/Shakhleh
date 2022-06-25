using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SearchShipmentsByCompany : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentForTruckersLtd_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("TRUCKERS LTD");
            ClickButton("Clear Shipment Level Search");

            Set("Company name").To("Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");
            Click("Search");
            ExpectNoRow("TRUCKERS LTD");
        }
    }
}