using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompaniesAddedByWWLNotVisibleForTruckers : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112152")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {

            //These fields are not on the Customer side on Shipment Details


            Run<JohnSmithAddsShipmentForTruckersLtd,AddConsignmentWithAddedCompaniesAsWWL>();
            LoginAs<JohnSmithCustomer>();
            Click("New Shipment");
            ClickLabel("Primary contact");
            Click("Jim Stevens");
            ClickField("Customer Reference");
            Near("Out of UK").ClickLabel("Into UK");
            Set("Vehicle number").To("187");
            Click("Save and Add/Amend Consignments");
            Click("New consignment");

            // check companies appear in the 3 fields
            Set("UK Trader").To("");
            ClickField("UK Trader");
            Type("WWL");
            ExpectNo(What.Contains, "WWL UK Trader");
            ExpectNo(What.Contains, "WWL Partner");
            ExpectNo(What.Contains, "WWL Declarant");

            Press(Keys.Tab);

            ClickField("Partner name");
            Type("WWL");
            ExpectNo(What.Contains, "WWL UK Trader");
            ExpectNo(What.Contains, "WWL Partner");
            ExpectNo(What.Contains, "WWL Declarant");

            Press(Keys.Tab);

            ClickField("Declarant");
            Type("WWL");
            ExpectNo(What.Contains, "WWL UK Trader");
            ExpectNo(What.Contains, "WWL Partner");
            ExpectNo(What.Contains, "WWL Declarant");
        }
    }
}
