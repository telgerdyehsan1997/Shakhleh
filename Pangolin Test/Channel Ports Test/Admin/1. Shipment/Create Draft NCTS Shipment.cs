using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateDraftNCTSShipment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517";
            var primaryContact = "Jack Smith";
            var customerRef = "CUSTOMERREFOUT1";
            var vehicleNumber = "123456";
            var trailerNumber = "7890";
            var dateOfDeparture = "03/08/2021";
            var shipmentRoute = "Blackpool to CALAIS";
            var officeOfDestination = "IT IT IT112345 ITALY";
            var trackingNumber = "1000000";

            Run<AddRouteBlackpoolAndCalais, CreateNewOfficeOfTransit_Italy>();

            LoginAs<ChannelPortsAdmin>();

            //Navigate to NCTS Shipments
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            //Navigate to new NCTS Shipment
            ClickLink("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            //Input Shipment Details
            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, primaryContact);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, primaryContact);
            Set("Customer Reference").To(customerRef);
            Set("Vehicle number").To(vehicleNumber);
            Set("Trailer number").To(trailerNumber);
            Set("Expected date of departure").To(dateOfDeparture);
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, shipmentRoute);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, shipmentRoute);
            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, officeOfDestination);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, officeOfDestination);
            ClickButton("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");

            //Asserts that Shipment has been created
            ClickLink("NCTS Shipments Out of UK");
            this.FindNCTSShipment(trackingNumber);
            ExpectRow(trackingNumber);
        }
    }
}