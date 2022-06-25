using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddRemainingDetailsToDeltaTransport : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyViaConsignmentsPageDeltaTransportLtd>();

            LoginAs<ChannelPortsAdmin>();
            Click("Companies");

            WaitToSeeHeader(That.Contains, "Companies");
            AtRow(That.Contains, "Delta Transport Ltd").Column("Customer account number").ExpectText("");

            AtRow(That.Contains, "Delta Transport Ltd").Column("Edit").Click("Edit");
            WaitToSeeHeader("Record Details");
            Set("Branch identifier").To("AG234");
            Set("AEO number").To("11111111111111111111");
            //Set("TSP").To("22222222222222222222");
            AtLabel("Guarantor Type").ClickLabel("Own");
            Set("Transit Guarantee").To("777");
            Set("Guarantee type").To("3");
            Set("TIN").To("888");
            Set("PIN").To("999");
            Click("Save");

            //AtRow(That.Contains, "Delta").Column("Customer account number").Expect("D55555");
        }
    }
}