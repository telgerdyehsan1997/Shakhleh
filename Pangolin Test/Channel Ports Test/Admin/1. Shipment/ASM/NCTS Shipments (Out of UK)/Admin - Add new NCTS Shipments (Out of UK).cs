using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddNewNCTSShipments_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewTransitOfficePL, AddRouteBlackpoolAndCalais, AdminAddsCompanyTruckersLtd, AddNewContactForTruckers_AlanSmith>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");

            ExpectHeader("Shipment Details");

            AtLabel("Is this a bulk shipment?").ClickLabel("No");

            Click("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ALAN SMITH");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ALAN SMITH");

            ClickLabel("Not required");

            Set("Customer Reference").To("30111");
            Set("Vehicle number").To("t37");
            Set("Trailer number").To("t37");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7913456789");

            Set("Expected date of departure").To("10/07/2022");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Blackpool to CALAIS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Blackpool to CALAIS");

            ExpectNoLabel(That.Equals, "Authorised location");
            RightOfLabel(That.Contains, "EU port of arrival / Transit").Expect(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");

            ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "PL SZCZECIN PL987654 POLAND");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "PL SZCZECIN PL987654 POLAND");

            ExpectNoLabel(That.Equals, "Authorised location");

            Click("Save and Add/Amend Consignments");
            ExpectHeader("Consignment Details");
            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date Created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("10/07/2022");
            Set(The.Bottom, "to").To("10/07/2022");
            Click("Search");

            AtRow("10/07/2022").Column("Expected date of departure").Expect("10/07/2022");
            AtRow("10/07/2022").Column("Route").Expect("Blackpool to Calais");
            AtRow("10/07/2022").Column("Customer Reference").Expect("30111");
            AtRow("10/07/2022").Column("Company name").Expect("Truckers Ltd");
            AtRow("10/07/2022").Column("Vehicle number").Expect("t37");
            AtRow("10/07/2022").Column("Trailer number").Expect("t37");
            AtRow("10/07/2022").Column("Progress").Expect("Draft - Normal");
        }
    }
}
