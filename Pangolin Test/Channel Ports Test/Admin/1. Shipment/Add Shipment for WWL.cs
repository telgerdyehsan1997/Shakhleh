using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentForWWL : UITest
    {
        [TestProperty("Sprint", "1")]
        [TestProperty("AMP", "112781")]
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126866")]
        [TestCategory("File Upload To Be Added")]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRoutePortsmouthToCalais, AddCompanyUserForWWLJenny, AdminAddsExchangeRates, OfficeOfTransitES>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            Click("New Shipment");

            ClickField("Company name");
            Type("Worldwide Logistics");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Worldwide");

            ClickHeader("Shipment Details");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Jenny Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Jenny Smith");
            //ClickLabel("Specific contacts");
            //Click(What.Contains, "Nothing selected");
            //Type("Jen");
            //System.Threading.Thread.Sleep(3000);
            //Press(Keys.Enter);

            //Set("Customer Reference").To("CR1");
            ClickLabel("Out of UK");
            // RightOf("NCTS").Expect("No");
            // Set port od departure
            Set("Vehicle number").To("VH1");
            Set("Trailer number").To("TR1");
            Set("Expected date of departure").To("02/01/2022");
            Set("Customer Reference").To("RT564744");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Portsmouth to CALAIS");

            /*ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM"); */

            ClickButton(That.Contains, "Save and Add/Amend Consignments");

            Click("Shipments");
            WaitToSeeHeader("Shipments");
            ClickLabel(The.Top, "All");
            Set("Date created").To("01/01/2020");
            Set("Expected date of arrival/departure").To("01/01/2020");
            Set(The.Top, "to").To("02/01/2022");
            Set(The.Bottom, "to").To("02/01/2022");
            ClickButton("Search");
            ExpectRow("Worldwide Logistics Ltd");

            //ExpectRow("T0719000001"); // BC its out of the UK

            AtRow("02/01/2022").Column("Type").Expect("Out of UK");
            AtRow("02/01/2022").Column("Expected date of arrival/departure").Expect("02/01/2022");
            //AtRow("02/01/2022").Column("Port of arrival/departure").Expect("Portsmouth");

        }
    }
}
