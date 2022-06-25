using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckNCTSShipmentOutDateFilterForCustomer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewNCTSShipmentOutOfUK, AddNewNCTSShipmentOutOfUKWithGroupNotifications>();
            LoginAs<JohnSmithCustomer>();
            Click("NCTS Shipments Out of UK");

            //date from search checks date of arrival/ departure

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow(That.Contains, "15/07/2019");
            ExpectNoRow(That.Contains, "10/07/2019");

            //date to search checks date of arrival/ departure

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectNoRow(That.Contains, "15/07/2019");
            ExpectRow(That.Contains, "10/07/2019");

        }
    }
}
