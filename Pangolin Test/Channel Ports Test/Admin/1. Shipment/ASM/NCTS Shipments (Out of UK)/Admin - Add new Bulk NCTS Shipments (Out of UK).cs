using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddNewBulkNCTSShipments_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRouteBlackpoolAndCalais, AdminAddsCompanyTruckersLtd, AddNewContactForTruckers_AlanSmith, CreateNewOfficeOfTransit_Italy>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment Details");

            ExpectNo("Consignor");
            ExpectNo("Consignee");
            ExpectNo("LRN");

            //Set shipment details to bulk shipment
            AtLabel("Is this a bulk shipment?").ClickLabel("Yes");

            Expect("Consignor");
            Expect("Consignee");

            ClickHeader("Shipment details");
            ClickField("Consignor");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Channel Ports - Hythe - CT21 4BL - GB683470514001 - 1234657");

            ClickField("Consignee");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "IMPORTS LTD - ROME - AG2 YGD - IL859098859098 - 6234517");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "IMPORTS LTD - ROME - AG2 YGD - IL859098859098 - 6234517");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, "Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Jack Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Jack Smith");

            ClickLabel("Not required");

            Set("Customer Reference").To("30111");
            Set("Vehicle number").To("t37");
            Set("Trailer number").To("t37");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7913456789");

            Set("Expected date of departure").To("10/07/2021");
            Set("Route").To("Cal");
            Click(What.Contains, "Blackpool");

            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "IT IT IT112345 ITALY");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "IT IT IT112345 ITALY");

            ExpectNoLabel("Authorised location");
            Click("Save and Add/Amend Consignments");
            ExpectHeader(That.Contains, "Bulk Consignment Details");

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date Created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("04/07/2022");
            Set(The.Bottom, "to").To("04/07/2022");
            Click("Search");

            AtRow("1000000").Column("Date").Expect("01/07/2021");
            AtRow("1000000").Column("Expected date of departure").Expect("10/07/2021");
            AtRow("1000000").Column("Route").Expect("Blackpool to Calais");
            AtRow("1000000").Column("Customer Reference").Expect("30111");
            AtRow("1000000").Column("Company name").Expect("Imports Ltd");
            AtRow("1000000").Column("Vehicle number").Expect("t37");
            AtRow("1000000").Column("Trailer number").Expect("t37");
            AtRow("1000000").Column("Progress").Expect("Draft - Normal");

        }
    }
}
