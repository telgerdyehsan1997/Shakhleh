using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckShipmentGuaranteeLengthDefaultsTo8 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewTransitOfficePL, AddRouteBlackpoolAndCalais, AdminAddsCompanyTruckersLtd,
                AddNewContactForTruckers_AlanSmith, AddAuthorisedLocationsToCompanyTruckers>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");

            ExpectHeader("Shipment Details");
            AtLabel("Is this a bulk shipment?").ClickLabel("No");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Truckers");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Truckers");

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
            Set("Route").To("Cal");
            Click(What.Contains, "Blackpool");

            ExpectNoLabel(That.Equals, "Authorised location");
            RightOfLabel(That.Contains, "EU port of arrival / Transit").Expect(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");
            Set(That.Contains, "Office of Destination").To("PL");
            Click(The.Bottom, "PL SZCZECIN PL987654 POLAND");

            AtLabel("Use authorised location").ClickLabel("Yes");
            ClickField("Select authorised location");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Stop 24");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Stop 24");

            //Asserts that Guarantee length defaults to 8
            ExpectLabel("Guarantee length");
            AtLabel("Guarantee length").Expect("8");
        }
    }
}
