using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompanyAddedByAdminButNotYetAddedToTruckersNotVisibleToTruckers : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112152")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            // Add consignment to truckers as admin - add 3 different companies but not saving consignment using new companies - not visible when adding this one as Customer

            Run<AddCompaniesInConsignmentsForm, JohnSmithAddsShipmentForTruckersLtd>();
            LoginAs<JohnSmithCustomer>();
            Set("Date created").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            ClickButton("Search");
            AtRow(That.Contains, "01/01/2019").Click("Edit");

            Click("Save and Add/Amend Consignments");
            Click("New consignment");

            Press(Keys.Tab);

            ClickField("UK Trader");
            Type("Ltd");
            ExpectNo(What.Contains, "Alpha Transport Ltd");
            ExpectNo(What.Contains, "Omega Transport Ltd");
            ExpectNo(What.Contains, "Delta Transport Ltd");

            Press(Keys.Tab);

            ClickField("Partner name");
            Type("Ltd");
            ExpectNo(What.Contains, "Alpha Transport Ltd");
            ExpectNo(What.Contains, "Omega Transport Ltd");
            ExpectNo(What.Contains, "Delta Transport Ltd");

            Press(Keys.Tab);

            ClickField("Declarant");
            Type("Ltd");
            ExpectNo(What.Contains, "Alpha Transport Ltd");
            ExpectNo(What.Contains, "Omega Transport Ltd");
            ExpectNo(What.Contains, "Delta Transport Ltd");
        }
    }
}
