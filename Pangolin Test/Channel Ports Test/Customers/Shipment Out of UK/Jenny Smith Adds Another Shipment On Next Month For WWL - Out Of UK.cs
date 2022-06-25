using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsAnotherShipmentOnNextMonthForWWL_OutOfUK : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]
        [TestCategory("File Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd,AddCompanyUserForWWLJenny,AddCompanyUserForWWLRichardSmith,JennySmithAddsAnotherShipmentWithinSameMonthForWWL_OutOfUK>();
            LoginAs<JennySmithCustomer>();

            AssumeDate("01/03/2020");
            CheckBackgroundTasks();
            AtRow("Reset shipment tracking number").Click("Execute");
            ClickXPath("/html/body/table/tbody/tr[2]/td[2]/a");
            Goto("/");

            Click(The.Top, "Shipments Out of UK");
            WaitToSeeHeader("Shipments Out of UK");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");

            Set("Primary contact").To("Richard");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Richard");

            Set("Customer Reference").To("34567");
            ClickLabel("Out of UK");
            Set("Vehicle number").To("43210");
            Set("Trailer number").To("56789");
            Set("Expected date of departure").To("01/03/2020");
            Set(The.Top, "Choose file").To("Example.png");
            Click("Add Another Attachment");
            Set(The.Bottom, "Choose file").To("Example.png");
            Set("Port of departure").To("Ports");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Portsmouth");
            Click("Save and Add/Amend Consignments");

            ExpectHeader("Consignment Details");
            ClickLink("Cancel");
            ExpectText("There are no consignments to display.");
            ClickLink("Shipments Out of UK");

            ExpectRow("T0320000001");
            AtRow("T0320000001").Column("Date").Expect("01/03/2020");
            AtRow("T0320000001").Column("Customer Reference").Expect("34567");
            AtRow("T0320000001").Column("Company name").Expect("Worldwide Logistics Ltd");
            AtRow("T0320000001").Column("Vehicle number").Expect("43210");
            AtRow("T0320000001").Column("Trailer number").Expect("56789");
            AtRow("T0320000001").Column("Progress").Expect("Draft");
            AtRow("T0320000001").Column("Weights mismatch").ExpectNoTick();
        }
    }
}
