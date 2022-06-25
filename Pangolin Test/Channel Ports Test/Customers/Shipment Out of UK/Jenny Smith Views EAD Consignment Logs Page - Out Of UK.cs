using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithViewsEADConsignmentLogsPage_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddConsignmentForWWL, AddCompanyUserForWWLJenny>();
            LoginAs<JennySmithCustomer>();
            AssumeDate("01/02/2020");
            Goto("/");

            Click("Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");


            WaitToSeeHeader("Shipments Out of UK");
            AtRow(That.Contains, "Worldwide").Column("Tracking number").ClickLink();

            WaitToSeeHeader("Shipment Details");

            ExpectRow("T072100000101");
            AtRow("T072100000101").Column("Actions").Click("Select action");
            System.Threading.Thread.Sleep(1000);
            ClickLink(That.Contains, "View Logs");

            ExpectHeader(That.Contains, "Logs");

            ExpectButton("Back");
        }
    }
}
