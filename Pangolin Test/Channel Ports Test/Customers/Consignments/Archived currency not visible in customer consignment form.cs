using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchivedCurrencyNotVisibleInCustomerConsignmentForm : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JennySmithAddsShipmentForWWL_OutOfUK,ArchiveCurrencyAUD>();
            LoginAs<JennySmithCustomer>();

            AssumeDate("01/02/2022");
            Goto("/");

            Click("Shipments Out of UK");
            WaitToSeeHeader("Shipments Out of UK");
            AtRow("T0220000001").Click("Edit");
            Click("Save and Add/Amend Consignments");

            // add consignment
            Click("New consignment");
            WaitToSeeHeader("Consignment Details");
            ClickField("Invoice currency");
            Type("AUD");
            ExpectNo("AUD");
            Expect("Not found");
        }
    }
}
