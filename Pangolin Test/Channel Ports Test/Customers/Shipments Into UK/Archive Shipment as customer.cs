using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveShipmentAsCustomer : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount, JohnSmithAddsShipmentForTruckersLtd>();
            LoginAs<JohnSmithCustomer>();
            AssumeDate("1/1/2019");
            Goto("/");

            //assert 'new Shipment' layout
            WaitToSeeHeader("Shipments Into UK");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "6514").Click("Archive");
            ExpectHeader(The.Top,"Archive");
            Click("Cancel");
            AtRow(That.Contains, "6514").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archived");
            ClickButton("Archive");

            ExpectNoRow(That.Contains, "6514");

        }
    }
}
