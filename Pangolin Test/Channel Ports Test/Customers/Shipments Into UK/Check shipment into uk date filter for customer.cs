using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckShipmentIntoUkDateFilterForCustomer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JennySmithAddsAnotherShipmentWithinSameMonthForWWL_IntoUK>();

            LoginAs<JennySmithCustomer>();
            Click("Shipments Into UK");

            //date from search checks date of arrival/ departure

            Set("Date Created").To("12/02/2020");
            Set("Expected date of arrival/departure").To("12/02/2020");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            Click("Search");
            ExpectRow(That.Contains, "11/07/2020");
            ExpectNoRow(That.Contains, "01/03/2020");

            //date to search checks date of arrival/ departure

            Set("Date Created").To("12/02/2016");
            Set("Expected date of arrival/departure").To("12/02/2016");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            Click("Search");
            ExpectRow(That.Contains, "11/07/2020");
            ExpectRow(That.Contains, "01/03/2022");
        }
    }
}