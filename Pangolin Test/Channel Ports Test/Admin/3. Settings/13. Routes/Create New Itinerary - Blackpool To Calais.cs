using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewItinerary_BlackpoolToCalais : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var itineraryCountry = "Greece";

            Run<AddRouteBlackpoolAndCalais>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Routes section
            this.navigateToRoutes();

            //Navigate to create New Itinerary
            AtRow("Blackpool").Column("Itineraries").ClickLink();
            ExpectHeader("Itineraries");
            ClickLink("New Itinerary");
            ExpectHeader("Itinerary Details");

            //Sets the new Itinerary details 
            Click(What.Contains, "---Select---");
            Expect(What.Contains, itineraryCountry);
            Click(What.Contains, itineraryCountry);
            ClickButton("Save");
        }
    }
}