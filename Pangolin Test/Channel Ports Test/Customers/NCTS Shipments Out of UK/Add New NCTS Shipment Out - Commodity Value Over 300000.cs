using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewNCTSShipmentOut_CommodityValueOver300000 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<SetNCTSHighValueThresholdTo300K, JohnSmithCreatesACustomerAccount, AddNewContactForTruckers_AlanSmith, TruckersUsesChannelPortsGuarantee, AddNewContactGroup_Import, AddRouteSouthamptonAndValencia, CreateNewTransitOfficePL>();
            LoginAs<JohnSmithCustomer>();

            //Navigates to NCTS Shipments Out of UK
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            ClickLink("New NCTS Shipment");
            ExpectHeader("Shipment details");
            AtLabel("Is this a bulk shipment?").ClickLabel("No");

            //Sets the Shipment Details
            //ClickField("Company name");
            //System.Threading.Thread.Sleep(1000);
            //Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            //System.Threading.Thread.Sleep(1000);
            //Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickLabel("No");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALAN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALAN SMITH");

            Set("Customer Reference").To("CustomerRef1");
            Set("Vehicle number").To("1234567");
            Set("Trailer number").To("7654321");
            Set("Expected date of departure").To("11/07/2022");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Southampton to VALENCIA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Southampton to VALENCIA");

            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "PL SZCZECIN PL987654 POLAND");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "PL SZCZECIN PL987654 POLAND");

            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");

            //Sets the Consignment details
            Set("EAD MRN").To("20GB07X87495234017");
            AtLabel("Import EAD Commodities").ClickLabel("No");
            Click("Search");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "United Kingdom");

            Set("Total packages").To("1");
            Set("Total gross weight").To("1");
            Set("Total net weight").To("1");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("300001");
            Click("Save and Add Commodities");

            Expect(What.Contains, "The value is more than £300000");
            ClickButton("OK");

            //Adds a New Commodity
            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212");

            Set("Description of goods").To("Stuff");
            Set("Gross weight").To("1");
            Set("Net weight").To("1");
            Set("Value").To("300001");
            Set("Number of packages for this commodity code (if known)").To("1");
            Click("Save");
            ExpectNoButton("Transmit");

            //Asserts that Progress is on Hold
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");
            ExpectRow("1000000");
            //AtRow("1000000").Column("Progress").Expect("On Hold - Due to Value");

            //Asserts that email is sent for Consignment value being on hold
            CheckMailBox("");
            ExpectRow(That.Contains, "Consignment is on hold due to exceed amount,Your reference CUSTOMERREF1");
            AtRow(That.Contains, "Consignment is on hold due to exceed amount,Your reference CUSTOMERREF").Column("Subject").ClickLink();
            ExpectHeader("Subject: Consignment is on hold due to exceed amount,Your reference CUSTOMERREF1");
        }
    }
}