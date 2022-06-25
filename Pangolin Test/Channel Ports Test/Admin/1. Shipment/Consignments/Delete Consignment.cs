using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DeleteConsignment : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentForWWL>();

            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "RT564744").Column("Edit").Click("Edit");
            WaitToSee("Shipment Details");
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");

            ExpectRow("T072100000101");
            AtRow("T072100000101").Click("Delete");

            Expect("Are you sure you want to delete this consignment?");
            Click("Ok");

            //ExpectNoRow("T071900000101");
        }
    }
}