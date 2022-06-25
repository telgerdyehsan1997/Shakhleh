using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Itinerary_AssertThatBlankItineraryCantBeSet : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRouteBlackpoolAndCalais>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to settings
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Routes");
            ExpectHeader("Routes");

            //Navigates to the Itineraries page for the route
            AtRow("Blackpool").Column("Itineraries").Click("0");
            ExpectHeader("Itineraries");

            //Attempts to add a blank Itinerary
            ClickLink("New Itinerary");
            ExpectHeader("Itinerary Details");
            ClickButton("Save");
            Expect(What.Contains, "The Destination country field is required"); //Will be updated when validation is implemented
        }
    }
}