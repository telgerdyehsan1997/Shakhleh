using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddShipmentForTruckersLtd_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321";
            var routeName = "Portsmouth to CALAIS";
            var primaryContact = "John Smith";

            Run<AddRoutePortsmouthToCalais, AdminAddsCompanyTruckersLtd, CreateNewCompanyUser_JohnSmith, AddNewContactGroup_Import, EditTruckersToBeForwarder>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            ClickLink("New Shipment");

            WaitToSeeHeader("Shipment Details");

            ClickHeader("Shipment Details");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            AtLabel("Type").ClickLabel("Out of UK");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, routeName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, routeName);

            AtLabel("Company type").Expect("Forwarder");

            Set("Primary contact").To("");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, primaryContact);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, primaryContact);

            ClickLabel("Group");
            Set(The.Bottom, "Group").To("IMPORT");
            Set("Customer Reference").To("RT564744");

            Set("Vehicle number").To("T37");
            Set("Trailer number").To("T87");
            Set("Expected date of departure").To("04/10/2022");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Portsmouth to CALAIS");

            AtLabel("NCTS").ClickLabel("No");

            /* ClickField("Port of departure");
             Type("Por");
             Click(The.Bottom, "Portsmouth"); */
            ClickButton(That.Contains, "Save and Add/Amend Consignments");
            //ClickButton(That.Contains, "Save");

            ExpectHeader(That.Contains, "Consignment Details");
            Click("Shipments");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("04/10/1999");
            Set("Expected date of arrival/departure").To("04/10/1999");
            Set(The.Bottom, "to").To("04/10/2030");
            Set(The.Top, "to").To("04/10/2030");
            Click("Search");

            ExpectRow(That.Contains, "T0721000001");
        }
    }
}
