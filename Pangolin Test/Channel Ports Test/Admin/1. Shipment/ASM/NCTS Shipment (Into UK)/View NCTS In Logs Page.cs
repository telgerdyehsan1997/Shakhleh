using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewNCTSInLogsPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipment_IntoUK>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("99MBABCDEFG1234567");
            ExpectColumn("Logs");

            AtRow("99MBABCDEFG1234567").Column("Logs").ClickLink();

            ExpectHeader(That.Contains, "Logs");

            ExpectButton("Back");
        }
    }
}