using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveShipmentAsCustomer : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveShipmentAsCustomer>();

            LoginAs<JohnSmithCustomer>();

            AssumeDate("1/1/2019");
            Goto("/");

            //assert 'new Shipment' layout
            WaitToSeeHeader("Shipments Into UK");
            ClickLabel("Archived");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow(That.Contains, "6514").Click("Unarchive");
            ExpectHeader("Unarchive");
            ClickButton("Cancel");
            AtRow(That.Contains, "6514").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive Reason");
            ClickButton("Unarchive");

            ExpectNoRow(That.Contains, "6514");
        }
    }
}