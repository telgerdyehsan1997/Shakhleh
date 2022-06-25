using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertThatCustomerIsGuarantor : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321";
            var ukTrader = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";
            var primaryContact = "John Smith";
            var shipmentRoute = "Blackpool to CALAIS";
            var officeOfDestination = "PL SZCZECIN PL987654 POLAND";

            Run<CreateNewTransitOfficePL, AddRouteBlackpoolAndCalais, AdminAddsCompanyTruckersLtd, AddKazumaKiryuToMajimaConstruction, AddCompanyMajimaConstruction_DefNumberStartsWith2, CreateNewCompanyUser_JohnSmith>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to NCTS Shipments
            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment Details");

            //Sets Shipment Details
            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickField("Company Name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, primaryContact);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, primaryContact);

            Set("Customer Reference").To("CusRef1");
            Set("Vehicle number").To("1234");
            Set("Expected date of departure").To("01/07/2021");

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

            //Saves the Shipment
            Click("Save and Add/Amend Consignments");

            //Navigates to the Consignment page to view the Guarantor
            ExpectHeader("Consignment Details");
            Set("EAD MRN").To("12GB45678945612349");
            ClickButton("Search");

            //Adds the UK Trader to see if the Guarantor matches the UK Trader
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, ukTrader);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, ukTrader);

            //Asserts that the customer is the Guarantor since the UK Trader has no Transit Guarantee 
            AtLabel("Guarantor").Expect("TRUCKERS LTD");
        }
    }
}