using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class RPTID_AssertEORINumberValidation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var trackingNumber = "R0721000001";
            var consignmentNumber = "R072100000101";
            var commodity = new Constants.CommodityFactory().AddMajimaConstructionCommodityIntoUK();

            Run<AddLicenceStatusCode_IntoUK, AddConsignmentToMajimaConstruction_IntoUK>();
            LoginAs<ChannelPortsAdmin>();

            //Finds the Shipment
            this.FindShipment(trackingNumber);

            //Navigate to Commodity Page for Shipment
            AtRow(trackingNumber).Column("Consignments").ClickLink("1");

            ExpectRow(consignmentNumber);
            AtRow(consignmentNumber).Column("Commodities").ClickLink("0");

            ExpectLink("New Commodity");
            ClickLink("New Commodity");

            ExpectHeader("Commodity Details");

            //Adds the Commodity
            ClickHeader("Commodity Details");
            ClickField("Product Code");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, commodity.Product);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, commodity.Product);
            Set("Gross weight").To(commodity.GrossWeight);
            Set("Net weight").To(commodity.NetWeight);
            Set("Second quantity").To(commodity.SecondQuantity);
            Set("Value").To(commodity.Value);
            Set("Number of packages for this commodity code (if known)").To(commodity.NumberOfPackages);
            this.ClickAndWait("Country of origin", commodity.Country);
            AtLabel("Preference").ClickLabel(commodity.HasPreference);
            AtLabel("Preference type").ClickLabel(commodity.PreferenceType);

            //Enables the RPTID Code
            AtLabel("Are the goods licencable?").ClickLabel("Yes");
            this.ClickAndWait("Licence name", "Local Import Licence");
            Set("Licence number").To("123");

            //Sets the RPTID Code to an invalid value EORI Value (not 14 characters)
            Set("RPTID code").To("GB68347051");
            ClickButton("Save");

            //Asserts validation
            Expect("The RPTID code must be 6 digits or should follow the format of a valid EORI Number.");
            ClickButton("OK");

            //Sets the RPTID to a value EORI
            Set("RPTID code").To("GB683470514001");
            ClickButton("Save");

            //Asserts that Commodity is saved
            ExpectRow("Greece");
        }
    }
}