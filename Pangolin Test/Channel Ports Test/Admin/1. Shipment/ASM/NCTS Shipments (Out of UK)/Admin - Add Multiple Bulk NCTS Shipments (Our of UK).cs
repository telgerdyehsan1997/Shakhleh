using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddMultipleBulkNCTSShipments_OurOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewbulkNCTSShipmentOutOfUK, Admin_AddA2ndNCTSShipments_OutOfUK, CreateNewOfficeOfTransit_Italy>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments Out of UK
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Creates new NCTS Shipment
            ClickLink("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");
            AtLabel("Is this a bulk shipment?").ClickLabel("No");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Import");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Import");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Jack Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Jack Smith");
            Set("Customer Reference").To("55655555");
            Set("Vehicle number").To("123");
            Set("Trailer number").To("4321");
            Set("Expected date of departure").To("18/06/2022");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Calais");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Calais");
            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ITALY");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ITALY");
            ClickButton("Save and Add/Amend Consignments");

            //Set Consignment Details
            ExpectHeader("Consignment Details");
            Set("EAD MRN").To("18GB12345342312376");
            ClickButton("Search");
            Set("UK trader").To("");
            ClickHeader("Consignment details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Italy");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Italy");

            Set("Total packages").To("1");
            Set("Total gross weight").To("1");
            Set("Total net weight").To("1");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GBP");
            Set("Total value").To("1");
            ClickButton("Save and Add Commodities");

            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");

            //Set Commodity Details
            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212");

            Set("Description of goods").To("Goods description");
            Set("Gross weight").To("1");
            Set("Net weight").To("1");
            Set("Value").To("1");
            Set("Number of packages for this commodity code (if known)").To("1");
            ClickButton("Save");
            //Error should occur after clicking save
        }
    }
}