using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCommodityToAConsignment : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<CreateNewCountry_France, AddConsignmentToTruckersLtd, AdminAddsProduct_IPad>();
            LoginAs<ChannelPortsAdmin>();


            AssumeDate("04/10/2022");
            Goto("/");

            this.FindShipment(trackingNumber);

            AtRow(trackingNumber).Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");

            // add commodities
            AtRow(consignmentNumber).Column("Commodities").ClickLink("0");

            WaitToSeeHeader(That.Contains, "R072100000101 - Commodities");
            ExpectNoButton("Complete");
            Click("New Commodity");

            WaitToSeeHeader("Commodity Details");
            ClickField("Product code");
            Type("IPad");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Gross weight").To("100");
            Set("Net weight").To("90");
            Set("Second quantity").To("1000");

            AtLabel("Currency").Expect("Great Britain - GBP");
            Set("Value").To("1000");
            Set("Number of packages for this commodity code (if known)").To("10");
            Set(The.Bottom, "Second quantity").To("50");
            ClickField("Country of origin");
            Type("Greece");
            System.Threading.Thread.Sleep(500);
            Click(What.Contains, "Greece");
            AtLabel("Preference").ClickLabel("Yes");
            AtLabel("Preference type").ClickLabel("Invoice declaration");
            Click(What.Contains, "Save");

            ExpectRow("ABS12343");
            AtRow("ABS12343").Column("Gross weight").Expect("100 kg");
            AtRow("ABS12343").Column("Net weight").Expect("90 kg");
            AtRow("ABS12343").Column("Currency").Expect("Great Britain - GBP");
            AtRow("ABS12343").Column("Value").Expect("1,000");
            AtRow("ABS12343").Column("Number of packages").Expect("10");
            AtRow("ABS12343").Column("Country of origin").Expect("Greece");
            //AtRow("ABS12343").Column("Preference").Expect("");
            Click("Complete");
            ExpectRow(consignmentNumber);
        }
    }
}
