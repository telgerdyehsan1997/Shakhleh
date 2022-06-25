using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditNCTSShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOutOfUKWithGroupNotifications>();

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("1000000").Column("Edit").ClickLink();

            Set("Customer Reference").To("41606");
            Set("Vehicle number").To("34LN");
            Set("Trailer number").To("6976");
            Set("Expected date of departure").To("06/07/2019");

            Click("Save and Add/Amend Consignments");
            Click("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("1000000").Column("Expected date of departure").Expect("06/07/2019");
            AtRow("1000000").Column("Customer Reference").Expect("41606");
            AtRow("1000000").Column("Vehicle number").Expect("34LN");
            AtRow("1000000").Column("Trailer number").Expect("6976");
        }
    }
}