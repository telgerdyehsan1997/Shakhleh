using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCommoditiesToSafetyAndSecurityConsignmentIntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var commodity = new Constants.CommodityFactory().AddMajimaConstructionCommodityIntoUKSandS();
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";

            Run<NewConsignment_SafetyAndSecurityIntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the shipment
            this.FindShipment(trackingNumber);

            //Navigates to the Commodities of the Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink("0");

            ExpectLink("New Commodity");
            ClickLink("New Commodity");

            ExpectHeader("Commodity Details");

            //Sets the Commodity Details
            ClickHeader("Commodity Details");
            System.Threading.Thread.Sleep(1000);
            ClickField("Product code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, commodity.Product);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, commodity.Product);
            Set("Gross weight").To(commodity.GrossWeight);
            Set("Net weight").To(commodity.NetWeight);
            Set(The.Bottom, "Second quantity").To(commodity.SecondQuantity);
            Set("Value").To(commodity.Value);
            Set("Number of packages for this commodity code (if known)").To(commodity.NumberOfPackages);
            this.ClickAndWait("Country of origin", commodity.Country);
            AtLabel("Preference").ClickLabel(commodity.HasPreference);
            AtLabel("Preference type").ClickLabel(commodity.PreferenceType);
            Click("Save");

            //Asserts that Commodity has been saved
            ExpectRow("Greece");
        }
    }
}