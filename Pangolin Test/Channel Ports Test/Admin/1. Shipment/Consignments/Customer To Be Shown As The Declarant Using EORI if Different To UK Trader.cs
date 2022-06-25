using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerToBeShownAsTheDeclarantUsingEORIIfDifferentToUKTrader : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsProduct_IPad, AddNewShipmentForTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("28/06/2021");
            Set("Expected date of arrival/departure").To("28/06/2021");
            Set(The.Top, "to").To("04/07/2021");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "R0721000001").Column("Consignments").Click("0");
            Click("New Consignment");

            //add new con
            WaitToSee(What.Contains, "Consignment Details");
            AtLabel("Consignment number").Expect("R072100000101");
            ClickField("UK trader");
            Expect(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");
            //RightOfLabel("Declarant").Expect("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            //Set("UK trader").To("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            //RightOfLabel("Declarant").Expect("CHANNEL PORTS"); Need to confirm
        }
    }
}
