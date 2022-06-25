using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckShipmentOutOfUKDateFilterForCustomer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JennySmithAddsAnotherShipmentWithinSameMonthForWWL_OutOfUK>();

            LoginAs<JennySmithCustomer>();
            Click("Shipments Out of UK");

            //date from search checks date of arrival/ departure

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectNoRow(That.Contains, "01/02/2020");
            ExpectRow(That.Contains, "15/02/2020");

            //date to search checks date of arrival/ departure

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow(That.Contains, "01/02/2020");
            ExpectNoRow(That.Contains, "15/02/2020");
        }
    }
}