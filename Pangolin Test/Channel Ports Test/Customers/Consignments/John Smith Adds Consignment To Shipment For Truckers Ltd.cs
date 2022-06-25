using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithAddsConsignmentToShipmentForTruckersLtd : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112152")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithAddsShipmentForTruckersLtd>();
            //navigate
            LoginAs<JohnSmithCustomer>();
            AssumeDate("1/1/2019");
            Goto("/");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0119000001").Column("Consignments").ClickLink();
            ClickLink("New Consignment");

            //add new consignment
            AtLabel("Consignment number").Expect("R011900000101");
            Set("UK trader").To("");
            ClickHeader("Consignment Details");
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

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");


            Set("Total packages").To("6");
            Set("Total gross weight").To("12");
            Set("Total net weight").To("9");
            Set("Invoice number").To("TRUCKERS-2019-0001");
            ClickField("Invoice currency");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Great Britain - GBP");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Great Britain - GBP");
            Set("Total value").To("1200");
            Set("Terms of Sale").To("EXW - Ex Works");
            Set("Freight currency").To("Great Britain - GBP");
            Set("Freight amount").To("100");

            Click(What.Contains, "Save");
            Click("Back");
            Click("Cancel");

            //assert new consignment
            WaitToSeeHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R011900000101").Column("Consignment number").Expect("R011900000101");
            AtRow(That.Contains, "R011900000101").Column("UK trader").Expect("Truckers Ltd");
            AtRow(That.Contains, "R011900000101").Column("Partner").Expect("TRUCKERS LTD");
            AtRow(That.Contains, "R011900000101").Column("Declarant").Expect("Channel Ports");
            AtRow(That.Contains, "R011900000101").Column("Total packages").Expect("6");
            AtRow(That.Contains, "R011900000101").Column("Total gross weight").Expect("12 kg");
            AtRow(That.Contains, "R011900000101").Column("Total net weight").Expect("9 kg");
            AtRow(That.Contains, "R011900000101").Column("Invoice number").Expect("TRUCKERS-2019-0001");
            AtRow(That.Contains, "R011900000101").Column("Invoice currency").Expect("Great Britain - GBP");
            AtRow(That.Contains, "R011900000101").Column("Total value").Expect("1,200.00");
            AtRow(That.Contains, "R011900000101").Column("Commodities").Expect("0");
        }
    }
}
