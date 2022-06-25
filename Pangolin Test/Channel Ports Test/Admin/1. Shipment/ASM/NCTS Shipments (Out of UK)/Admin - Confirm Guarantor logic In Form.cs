using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_ConfirmGuarantorLogicInForm : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";
            var primaryContact = "KAZUMA KIRYU";
            var shipmentRoute = "Blackpool to CALAIS";
            var officeOfDestination = "PL SZCZECIN PL987654 POLAND";

            Run<CreateNewTransitOfficePL, AddRouteBlackpoolAndCalais, AddCompanyMajimaConstruction_DefNumberStartsWith2, AddKazumaKiryuToMajimaConstruction>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();
            AtRow("MAJIMA CONSTRUCTION").Column("Edit").Click("Edit");
            ExpectHeader("Record Details");

            //Sets the guarantor to ChannelPorts
            AtLabel("Guarantor Type").ClickLabel("Different Company's Guarantee");
            ClickField("Guarantor Name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            ClickButton("Save");

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

            //Asserts that ChannelPorts is the Guarantor since the Company and UK has no Transit Guarantee 
            AtLabel("Guarantor").Expect("Channel Ports");
        }
    }
}
