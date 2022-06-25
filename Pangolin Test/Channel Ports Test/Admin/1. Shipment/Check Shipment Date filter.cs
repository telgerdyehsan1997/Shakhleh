using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckShipmentDateFilter : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewShipmentForTruckersLtd, AddShipmentForWWL, AddAnotherShipmentForTruckersWithin1Month, AddAnotherShipmentForWWLWithin1Month, AddAnotherShipmentForTruckersWithin1Month, AddAnotherShipmentForWWLWithin1Month>();
            LoginAs<ChannelPortsAdmin>();
            //check date that filter defaults to

            Near("Date").ExpectValue("01/07/2019");
            Near("to").ExpectValue("01/07/2019");

            //date from search checks date of arrival/ departure

            Set("Date").To("01/03/2020");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("01/03/2020");
            ExpectNoRow("01/02/2019");

            //date to search checks date of arrival/ departure

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectNoRow("01/03/2020");
            ExpectRow("31/01/2020");

        }
    }
}
