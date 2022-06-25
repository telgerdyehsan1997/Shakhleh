using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsAnotherShipmentOnNextMonthForWWL_IntoUK : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]
        [TestCategory("File Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny, AddCompanyUserForWWLRichardSmith, JennySmithAddsAnotherShipmentWithinSameMonthForWWL_IntoUK>();
            LoginAs<JennySmithCustomer>();

            AssumeDate("01/03/2020");

            CheckBackgroundTasks();
            AtRow("Reset shipment tracking number").ClickLink();
            Goto("/");

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CALAIS to Blackpool");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Blackpool");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "RICHARD SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "RICHARD SMITH");

            Set("Customer Reference").To("34567");
            ClickLabel("Into UK");
            Set("Vehicle number").To("43210");
            Set("Trailer number").To("56789");
            Set(The.Top, "Choose file").To("Example.png");
            Click("Add another attachment");
            Set(The.Bottom, "Choose file").To("Example.png");

            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("06/03/2020");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            WaitToSeeHeader(That.Contains, "Consignment Details");
            ClickLink("Cancel");
            Click("Back to Shipments");
            ClickLink("Shipments Into UK");

            //change to field
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            ExpectRow("R0320000001");
            AtRow("R0320000001").Column("Expected date of arrival/departure").Expect("06/03/2020");
            AtRow("R0320000001").Column("Customer Reference").Expect("34567");
            AtRow("R0320000001").Column("Company name").Expect(What.Contains, "Worldwide Logistics Ltd");
            AtRow("R0320000001").Column("Vehicle number").Expect("43210");
            AtRow("R0320000001").Column("Trailer number").Expect("56789");
            AtRow("R0320000001").Column("Progress").Expect("Draft");
            AtRow("R0320000001").Column("Weights mismatch").ExpectNoTick();
        }
    }
}
