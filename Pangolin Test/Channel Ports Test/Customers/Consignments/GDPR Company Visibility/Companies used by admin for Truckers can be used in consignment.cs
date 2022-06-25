using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompaniesUsedByAdminForTruckersCanBeUsedInConsignment : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112152")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
           
            // these fields are no longer on customer shipment form
            
            Run<JohnSmithCreatesACustomerAccount>();
            // add consignment to Truckers with 3 companies used - these are now visible when adding this one
            Run<AddACommodityWith4DifferentCompaniesUsed>();

            LoginAs<JohnSmithCustomer>();
            Click("New Shipment");
            Click("Nothing selected");
            Type("John");
            Press(Keys.Enter);

            Set("Customer Reference").To("123455");
            Near("Out of UK").ClickLabel("Into UK");
            Set("Vehicle number").To("89");
            Click("Save and Add/Amend Consignments");
            Click("New consignment");

            ClickField("UK Trader");
            Type("Ltd");
            Expect(What.Contains, "Alpha");
            Expect(What.Contains, "Delta");
            Expect(What.Contains, "Worldwide");
            ExpectNo(What.Contains, "Omega");

            Press(Keys.Tab);

            ClickField("Partner name");
            Type("Ltd");
            Expect(What.Contains, "Alpha");
            Expect(What.Contains, "Delta");
            Expect(What.Contains, "Worldwide");
            ExpectNo(What.Contains, "Omega");

            Press(Keys.Tab);

            ClickField("Declarant");
            Type("Ltd");
            Expect(What.Contains, "Alpha");
            Expect(What.Contains, "Delta");
            Expect(What.Contains, "Worldwide");
            ExpectNo(What.Contains, "Omega");
        }
    }
}