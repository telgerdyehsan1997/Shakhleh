using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckVehicleAndTrailerNumberValidations : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "#126875")]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewContactForTruckers_AlanSmith, AddNewContactGroup_Import, AddRouteBlackpoolAndCalais>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            Set("Company name").To("Truck");
            Click("Truckers Ltd - Worcester - WR5 3DA - GB683470514001 - 7654321");
            //Set("LRN").To("R021900001512342345678");
            ClickField("Primary contact");
            Type("Alan Smit");
            Click("Alan Smith");

            Set("Customer Reference").To("41222");
            Set("Vehicle number").To("AAAA");
            Set("Trailer number").To("AA12");
            //Set("Driver mobile country").To("GB (+44)");
            //Set("Driver mobile number").To("7912345678");

            Set("Expected date of departure").To("15/07/2022");
            Set("Route").To("Cal");
            Click("Blackpool to Calais");

            ExpectNoLabel(That.Equals, "Authorised location");

            Set("Office of Destination").To("GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");
            Click("GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");

            ClickButton("Save and Add/Amend Consignments");

            Click("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("01/07/2019").Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            ClickButton("Save and Add/Amend Consignments");

            ClickButton("EAD MRN Not Known");
            Set("Total packages").To("22");
            Set("Total gross weight").To("22222222.13222");
            Set("Total net weight").To("2.567667567");
            Set("Invoice currency").To("Great Britain - GBP");
            Press(Keys.ArrowDown);
            Press(Keys.Enter);

            ClickButton("Save and Add/Amend Consignments");

            ExpectHeader("Commodities");

            AtRow("Consignment total gross weight").Expect("22,222,222.14 kg");
            AtRow("Consignment total net weight").Expect("2.57 kg");






        }
    }
}
