using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertNewItinerariesPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddRouteBlackpoolAndCalais>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Routes section
            this.navigateToRoutes();

            //Asserts that new column has been added
            AtRow("Blackpool").Column("Itineraries").Expect("0");

            //Navigates to new page
            AtRow("Blackpool").Column("Itineraries").ClickLink();
            ExpectHeader("Itineraries");
            ClickLink("New Itinerary");

            //Assert new page
            ExpectHeader("Itinerary Details");
            ExpectLabel(The.Top, "Destination Country");
            ExpectLabel("Country 1");
            ExpectLabel("Country 2");
            ExpectButton("Add another");

            ExpectButton("Cancel");
            ExpectButton("Save");
        }
    }
}