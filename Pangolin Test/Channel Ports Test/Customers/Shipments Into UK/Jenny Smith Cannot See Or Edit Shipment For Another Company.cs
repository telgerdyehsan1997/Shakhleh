using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithCannotSeeOrEditShipmentForAnotherCompany : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny, JohnSmithAddsShipmentForTruckersLtd>();
            LoginAs<JohnSmithCustomer>();

            AssumeDate("01/01/2020");
            Goto("/");

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            ExpectRow("R0119000001");
            AtRow("R0119000001").Click("Edit");
            WaitToSeeHeader("Shipment Details");
            CopyUrl();

            LoginAs<JennySmithCustomer>();

            ExpectHeader("Shipments Into UK");

            ExpectNoRow("R0119000001");
        }
    }
}
