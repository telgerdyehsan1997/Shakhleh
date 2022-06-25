using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsAnotherShipmentWithinSameMonthForWWL_IntoUK : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]
        [TestCategory("File Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny, AddCompanyUserForWWLRichardSmith, JennySmithAddsShipmentForWWL_IntoUK>();
            LoginAs<JennySmithCustomer>();

            AssumeDate("15/02/2020");
            Goto("/");

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");

            ClickField("Primary contact");
            Type("Richard Smit");
            Click("Richard Smith");
            Set("Customer Reference").To("23456");
            ClickLabel("Into UK");
            Set("Vehicle number").To("43210");
            Set("Trailer number").To("56789");

            /*ClickField("Port of arrival");
            Type("Portsmout");
            Click("Portsmouth");*/

            ClickField("Route");
            Expect(What.Contains, "CALAIS to Blackpool");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Blackpool");

            ClickButton(That.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("11/07/2020");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            Click("Shipments Into UK");
            ExpectHeader("Shipments Into UK");
            Set("Date Created").To("12/02/2020");
            Set("Expected date of arrival/departure").To("12/02/2020");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0220000002");
            //AtRow("R0220000002").Column("Date").Expect("15/02/2020");

            AtRow("R0220000002").Column("Customer Reference").Expect("23456");
            AtRow("R0220000002").Column("Company name").Expect(What.Contains, "Worldwide Logistics Ltd");
            AtRow("R0220000002").Column("Vehicle number").Expect("43210");
            AtRow("R0220000002").Column("Trailer number").Expect("56789");
            AtRow("R0220000002").Column("Progress").Expect("Draft");
            AtRow("R0220000002").Column("Weights mismatch").ExpectNoTick();
        }
    }
}
