using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckDateFilterForNCTSShipments_OutOfUk : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<Admin_AddA2ndNCTSShipments_OutOfUK, Admin_AddNewNCTSShipments_OutOfUK>();
            LoginAs<ChannelPortsAdmin>();
            Click("NCTS Shipments Out of UK");

            //date from search checks date of arrival/ departure

            Set("Date").To("11/01/2022");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow(That.Contains, "15/07/2022");
            ExpectRow(That.Contains, "10/07/2022");


        }
    }
}
