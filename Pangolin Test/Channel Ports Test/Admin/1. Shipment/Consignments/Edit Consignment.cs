using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditConsignment : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddConsignmentToTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();

            //case passes without any issues, just adding this so I would be able to send update to sourcetree - Raf

            WaitToSeeHeader("Shipments");
            Goto("/");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            WaitToSeeHeader("Shipments");
            AtRow(That.Contains, "R0719000001").Column("Edit").Click("Edit");

            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSee(What.Contains, "Consignments");

            //edit consignment
            AtRow(That.Contains, "R071900000101").Column("Edit").Click("Edit");
            WaitToSee("Consignment Details");
            Set("UK trader").To("WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            Click(The.Bottom, "WORLDWIDE LOGISTICS LTD - WORCESTER - WR5 3DA - GB683470514001 - 1234567");
            Set("Partner name").To("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Click(The.Bottom, "TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Set("Declarant").To("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Click(The.Bottom, "TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Set("Total packages").To("5");
            Set("Total gross weight").To("7.35");
            Set("Total net weight").To("6.555");
            Set("Invoice number").To("WWLOGISTICS-2019-1101");
            Set("Invoice currency").To("GBP");
            Click(What.Contains, "GREAT BRITAIN - GBP");
            Set("Total value").To("250");
            ClickLabel(The.Top, "No");
            ClickLabel(The.Bottom, "No");
            Set("UCR").To("9GB987654312000-R071900000101");
            Click(What.Contains, "Save");

            //assert new details
            ExpectHeader(That.Contains, "Commodities");
            Near("Consignment total packages").Expect("5");
            Near("Consignment total gross weight").Expect("7.35 kg");
            Near("Consignment total net weight").Expect("6.56 kg");
            Near("Consignment total value").Expect("250");

        }
    }
}
