using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GroupsForOtherCompaniesNotVisible : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewAdminUser_AliceSpat, AddContactGroupForWWL, AddContactsToImport>();
            LoginAs<AliceSpatAdmin>();
            Click("Shipments");
            Click("New Shipment");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "TRUCKERS LTD - WORCESTER - WR5 3DA - 68GB3470514001 - 7654321");

            //Set("Notify additional party").To("Group");
            ClickLabel("Group");
            Click(The.Top, "---Select---");
            Expect("Import");
            ExpectNo("WWL Group");
        }
    }
}
