using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithAddsShipmentForWWL_OutOfUK : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]
        [TestProperty("AMP", "112781")]
        [TestCategory("File Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //Run<AddPortPortsmouth>();

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny, AddCompanyUserForWWLRichardSmith, AddRouteBlackpoolAndCalais>();
            LoginAs<JennySmithCustomer>();
            AssumeDate("01/02/2022");
            Goto("/");

            Click(The.Top, "Shipments Out of UK");
            WaitToSeeHeader("Shipments Out of UK");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");
            ClickField("Primary contact");
            Type("Richard Smith");
            Click(The.Bottom, "Richard Smith");
            Set("Customer Reference").To("12345");
            ClickLabel("Out of UK");
            Set("Vehicle number").To("43210");
            Set("Trailer number").To("56789");
            Set("Expected date of departure").To("01/02/2022");
            //Set("Port of departure").To("Blackpool");
            //Click("Blackpool");
            Set(The.Top, "Choose file").To("Example.png");
            Click("Add Another Attachment");
            Set(The.Bottom, "Choose file").To("Example.png");

            // Shipment going Out of UK

            Set("Expected date of departure").To("01/02/2022");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Blackpool to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Blackpool to CALAIS");
            AtLabel("NCTS").ClickLabel("No");

            /*ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM"); */

            ClickButton(That.Contains, "Save and Add/Amend Consignments");

            WaitToSeeHeader(That.Contains, "Consignment Details");
            Click("Shipments Out of UK");
            WaitToSeeHeader("Shipments Out of UK");

            ClearField(The.Bottom, "to");
            Click("Search");

            ExpectRow("T0222000001");
            //AtRow("T0222000001").Column("Date").Expect("01/02/2022");
            AtRow("T0222000001").Column("Expected date of arrival/departure").Expect("01/02/2022");
            //AtRow("T0120000001").Column("Port of arrival").Expect("");
            AtRow("T0222000001").Column("Customer Reference").Expect("12345");
            AtRow("T0222000001").Column("Company name").Expect("Worldwide Logistics Ltd");
            AtRow("T0222000001").Column("Vehicle number").Expect("43210");
            AtRow("T0222000001").Column("Trailer number").Expect("56789");
            AtRow("T0222000001").Column("Progress").Expect("Draft");
            AtRow("T0222000001").Column("Weights mismatch").ExpectNoTick();
        }
    }
}
