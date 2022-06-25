using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewCarrier_Amazon : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var settingsPage = "Carriers";
            var carrier = new Constants.CarrierFactory().CreateCarrierAmazon();

            LoginAs<ChannelPortsAdmin>();

            //Navigates to Carriers
            this.NavigateToSettingsPage(settingsPage); //Will uncomment once header of page has been fixed

            //Creates the new Carrier
            ClickLink("New Carrier");
            ExpectHeader("Carrier Details");
            Set("Carrier name").To(carrier.Name);
            Set("Address line 1").To(carrier.AddressLine1);
            Set("Address line 2").To(carrier.AddressLine2);
            Set("Town/City").To(carrier.Town);
            Set("Postcode/Zip code").To(carrier.Postcode);
            this.ClickAndWait("Country", carrier.Country);
            Set("EORI number").To(carrier.EORINumber);
            ClickButton("Save");

            //Asserts that carrier has been saved
            ExpectRow(carrier.Name);
        }
    }
}