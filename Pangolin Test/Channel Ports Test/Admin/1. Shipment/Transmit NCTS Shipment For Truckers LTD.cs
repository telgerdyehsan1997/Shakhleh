using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class TransmitNCTSShipmentForTruckersLTD : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewConsignmentToNCTSShipment, SetSendNCTSMessageViaASMToFalse>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("TRUCKERS LTD").Column("Tracking number").ClickLink();
            ExpectHeader("Shipment details");
        }
        //Manually set the LRN number in the database to either a correct or incorrect LRN number to test the HMRC response
        // UPDATE [channel.ports.Temp].[dbo].[NCTSShipmentOutConsignments]
        //		SET
        //			[LRN] = REPLACE([LRN], 'CP100000001', 'CP100041401');
    }
}
