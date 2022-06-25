using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSShipmentOutOfUKArchiveAndUseMRNAgain : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipentNCTSOutOfUKASMAcc>();

            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Expected date of arrival/departure").To("05/02/2022");
            Set(The.Bottom, "to").To("10/03/2022");
            Click("Search");
            ExpectRow("1000000");

            //add new Shipment with this same EAD MRN - 12GB45678945612345

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");
            AtLabel("Is this a bulk shipment?").ClickLabel("No");


            ClickField("Company name");
            Type("Geeks");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GEEKSQA");

            Set("Primary contact").To("TEST QA");
            Set("Customer reference").To("9596324");
            Set("Vehicle number").To("856123");
            Set("Trailer number").To("12367");
            Set("Expected date of departure").To("10/02/2022");

            ClickField("Route");
            Type("Port");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Portsmouth");

            ClickField("Office of Destination");
            Type("PL");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "PL SZCZECIN");

            Click("Save and Add/Amend Consignments");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            Set("EAD MRN").To("12GB45678945612345");
            Click("Search");

            ClearField("UK Trader");
            ClickField("UK Trader");
            Type("Geeks");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClearField("Partner name");
            ClickField("Partner name");
            Type("Geeks");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            Set("Country of destination").To("FRANCE");
            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice currency").To("GREAT BRITAIN - GBP");
            Set("Total value").To("10000");
            Click("Save and Add Commodities");
        }
    }
}