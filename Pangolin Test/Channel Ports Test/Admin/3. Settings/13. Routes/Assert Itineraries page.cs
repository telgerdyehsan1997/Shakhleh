using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AssertItinerariesPage : UITest
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

            //Asserts that labels and fields are included
            ExpectLink("New Itinerary");
            ExpectLabel("Destination Country");
        }
    }
}