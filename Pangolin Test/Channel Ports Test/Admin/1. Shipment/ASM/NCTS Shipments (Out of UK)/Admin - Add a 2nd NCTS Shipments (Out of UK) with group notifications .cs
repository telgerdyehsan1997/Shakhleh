using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddA2ndNCTSShipments_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewContactForTruckers_AlanSmith, AddNewContactGroup_Import, AddRouteBlackpoolAndCalais, CreateNewTransitOfficePL>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Alan Smith");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Alan Smith");

            System.Threading.Thread.Sleep(500);
            Set("Customer Reference").To("41222");
            Set("Vehicle number").To("2222");
            Set("Trailer number").To("4242");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7912345678");

            Set("Expected date of departure").To("15/07/2022");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Blackpool to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Blackpool to CALAIS");

            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "PL SZCZECIN PL987654 POLAND");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "PL SZCZECIN PL987654 POLAND");

            ExpectNoLabel(That.Equals, "Authorised location");

            //Set(That.Contains, "FirstBorderCrossing_Text").To("United Kingdom (GB) - GB000060");
            //lick(The.Bottom, "United Kingdom (GB) - GB000060");


            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");
            ClickButton("Cancel");
            ClickLink("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");

            AtRow("41222").Column("Route").Expect("Blackpool to Calais");
            AtRow("41222").Column("Customer Reference").Expect("41222");
            AtRow("41222").Column("Company name").Expect("Truckers Ltd");
            AtRow("41222").Column("Vehicle number").Expect("2222");
            AtRow("41222").Column("Trailer number").Expect("4242");
        }
    }
}
