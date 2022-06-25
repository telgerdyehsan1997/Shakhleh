using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ContactsForOtherCompaniesNotVisible : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCompanyUser_JohnSmith,AddCompanyUserForWWLJenny,CreateNewAdminUser_AliceSpat>();
            LoginAs<AliceSpatAdmin>();
            Click("Shipments");
            Click("New Shipment");
            WaitToSeeHeader("Shipment details");

            ClickField("Company name");
            Type("Truck");
            Click(What.Contains, "Truckers Ltd");

            ClickField("Primary contact");
            //Type("John");

            Click(What.Contains, "John Smith");

            ClickLabel("Specific contacts");
            Click(What.Contains, "Nothing selected");
            Expect("John Smith");
            ExpectNo("Jenny Smith");
        }
    }
}
