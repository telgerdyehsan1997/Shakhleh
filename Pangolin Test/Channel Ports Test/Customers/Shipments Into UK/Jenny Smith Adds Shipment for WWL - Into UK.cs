using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsShipmentForWWL_IntoUK : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]
        [TestProperty("AMP", "112781")]
        [TestCategory("File Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            // Run<AddPortPortsmouth>();

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny, AddCompanyUserForWWLRichardSmith, AddRouteBlackpoolAndCalais>();
            LoginAs<JennySmithCustomer>();
            AssumeDate("01/02/2020");
            Goto("/");

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");

            Set("Primary contact").To("Richard Smith");
            Click(The.Bottom, "Richard Smith");
            Set("Customer Reference").To("12345");
            ClickLabel("Into UK");
            Set("Vehicle number").To("43210");
            Set("Trailer number").To("56789");
            Set(The.Top, "Choose file").To("Example.png");
            Click("Add Another Attachment");
            Set(The.Bottom, "Choose file").To("Example.png");
            ClickLabel("Into UK");
            ClickField("Route");
            Expect(What.Contains, "CALAIS to Blackpool");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Blackpool");
            Set("Expected date of arrival").To("01/03/2022");

            /*ClickField("Port of arrival");
            Type("Por");
            System.Threading.Thread.Sleep(1000);
            Click("Portsmouth");*/

            ClickButton(That.Contains, "Save");

            WaitToSeeHeader("Consignment Details");
            Click("Cancel");
            ExpectHeader(That.Contains, "Consignments");
            Click("Back to Shipments");
            WaitToSeeHeader("Shipments Into UK");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");

            ExpectRow("R0220000001");
            AtRow("R0220000001").Column("Expected date of arrival/departure").Expect("01/03/2022");
            //AtRow("R0220000001").Column("Port of arrival").Expect("Portsmouth");
            AtRow("R0220000001").Column("Customer Reference").Expect("12345");
            AtRow("R0220000001").Column("Company name").Expect(What.Contains, "Worldwide Logistics Ltd");
            AtRow("R0220000001").Column("Vehicle number").Expect("43210");
            AtRow("R0220000001").Column("Trailer number").Expect("56789");
            AtRow("R0220000001").Column("Progress").Expect("Draft");
            AtRow("R0220000001").Column("Weights mismatch").ExpectNoTick();

        }
    }
}
