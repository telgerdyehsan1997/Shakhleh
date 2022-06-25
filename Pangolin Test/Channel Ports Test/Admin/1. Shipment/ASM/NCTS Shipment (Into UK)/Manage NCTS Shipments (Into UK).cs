using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageNCTSShipments_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewNCTSShipment_IntoUK, AddA2ndNCTSShipment_IntoUK>();
            LoginAs<ChannelPortsAdmin>();
            Click("NCTS Shipments Into UK");

            //date from search checks date of arrival/ departure

            Set("Date of arrival").To("02/07/2019");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow(That.Contains, "02/07/2019");
            ExpectNoRow(That.Contains, "01/07/2019");

            //date to search checks date of arrival/ departure

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            ExpectNoRow(That.Contains, "02/07/2019");
            ExpectRow(That.Contains, "01/07/2019");
        }
    }
}
