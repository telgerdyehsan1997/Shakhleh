using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SearchNCTSShipmentByCompany : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOutOfUK>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Company name").To("TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("1000000");
            Set("Company name").To("Shipping Company Ltd - Rome - FG6 YFD - SC859485859485 - 1234567");
            Click("Search");
            ExpectNoRow("1000000");
        }
    }
}