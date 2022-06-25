using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotEditSubmittedShipment : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsCommodityToConsignment>();

            //submit shipment
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date").To("01/07/2017");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Submit").ClickButton();
            WaitToSeeHeader("Shipments");

            //assert edit button invisible
            NearLabel("Into UK").ClickLabel("All");
            NearLabel("Submitted").ClickLabel("All");
            NearLabel("Archived").ClickLabel("All");
            Click("Search");
            WaitToSeeHeader("Shipments");
            ExpectRow(That.Contains, "R0119000001");
            AtRow(That.Contains, "R0119000001").Column("Edit").ExpectNoButton();
        }
    }
}