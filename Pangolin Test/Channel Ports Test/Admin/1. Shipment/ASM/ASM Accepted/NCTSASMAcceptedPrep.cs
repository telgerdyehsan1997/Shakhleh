using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSASMAcceptedPrep : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewCompanyUserRaf, OfficeOfTransitES, AddRoutePortsmouthToAmsterdam>();
            LoginAs<ChannelPortsAdmin>();
            ExpectHeader("Shipments");

            Click("Companies");
            AtRow("Imports Ltd").Column("Edit").Click("Edit");
            ClearField("Default declarant");
            ClickField("Default declarant");
            Type("Imports");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "Imports");
            Set("Transit Guarantee").To("1254693");
            Set("Guarantee type").To("A");
            Set("PIN").To("5846");
            AtLabel("CFSP").ClickLabel("None");

            Click("Save");

            //---------creating shipmnet------

            Click("NCTS Shipments Out of UK");
            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            AtLabel("Is this a bulk shipment?").ClickLabel("No");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "RAFAL QA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "RAFAL QA");

            AtLabel("Notify additional party").ClickLabel("Group");
            Set(The.Bottom, "Group").To("EMAIL ME");

            Set("Customer reference").To("7596324");
            Set("Vehicle number").To("RAF123");
            Set("Trailer number").To("1234567");
            Set("Expected date of departure").To("24/02/2022");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Portsmouth to AMSTERDAM");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Portsmouth to AMSTERDAM");

            ClickField("Office of Destination");

            System.Threading.Thread.Sleep(1000);
            Expect(The.Bottom, What.Contains, "ES MADRID ES001111 SPAIN");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ES MADRID ES001111 SPAIN");

            Click("Save and Add/Amend Consignments");

            //-------adding consignment-------

            ExpectHeader("Consignment Details");

            Set("EAD MRN").To("12GB33333333333333");
            Click("Search");

            ClickField("UK Trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "IMPORTS LTD - ROME - AG2 YGD - IL859098859098");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "IMPORTS LTD - ROME - AG2 YGD - IL859098859098");

            ClickField("Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "IMPORTS LTD - ROME - AG2 YGD - IL859098859098");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "IMPORTS LTD - ROME - AG2 YGD - IL859098859098");

            ClickField("Country of destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Greece");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Greece");

            Set("Total packages").To("100");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");

            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("10000");
            Click("Save and Add Commodities");

            //-----add commodity-----

            ClickLink("New Commodity");
            ExpectHeader("Commodity Details");

            ClickField("Commodity code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "12121212 - 14");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "12121212 - 14");
            Set("Description of goods").To("Aliens");
            Set("Gross weight").To("1000");
            Set("Net weight").To("900");
            Set("Value").To("10000");
            Set("Number of packages for this commodity code (if known)").To("100");

            Click("Save");

            ExpectButton("Complete");
            ClickButton("Complete");
        }
    }
}
