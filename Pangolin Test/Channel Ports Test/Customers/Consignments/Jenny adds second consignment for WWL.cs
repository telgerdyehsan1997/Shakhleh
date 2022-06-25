using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennyAddsSecondConsignmentForWWL : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnAddsSecondConsignmentForTruckersLtd>();

            LoginAs<JohnSmithCustomer>();

            ExpectHeader("Shipments Into UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("R0119000001").Column("Consignments").ClickLink("");

            ExpectRow("R011900000101");
            ExpectRow("R011900000102");

        }
    }
}