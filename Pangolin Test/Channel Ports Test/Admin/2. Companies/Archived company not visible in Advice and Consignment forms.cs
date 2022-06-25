using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [Ignore]
    [TestClass]
    public class ArchivedCompanyNotVisibleInAdviceAndConsignmentForms : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            //Pre-Advice workflow no longer exists so Test Case is redundant
            Run<AdminChangesTruckersLtdCompanyType>();

            LoginAs<ChannelPortsAdmin>();

            //check company is in advice form
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            Click("New Advice");
            WaitToSeeHeader("Advice Details");
            ClickField("Company name");
            Type("Ltd");
            Expect(What.Contains, "Truckers");

            //check company is in consignment form
            Click(The.Top, "Pre-Advice");
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            AtRow(The.Top).Column("Edit").Click("Edit");
            Click(What.Contains, "Nothing selected");
            Type("Jack");
            Press(Keys.Enter);
            Click("Save and Add/Amend Consignments");
            Click("New Consignment");
            ClickField("UK trader");
            Type("Ltd");
            Click(What.Contains, "Imports");
            ClickField("Partner name");
            Type("Ltd");
            Expect(What.Contains, "Truckers");

            //archive company
            Click(The.Top, "Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Archive").Click("Archive");
            WaitToSee("Are you sure you want to archive this company?");
            Click("OK");

            //check company is not in advice form
            Click(The.Top, "Pre-Advice");
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            Click("New Advice");
            WaitToSeeHeader("Advice Details");
            ClickField("Company name");
            Type("Ltd");
            ExpectNo(What.Contains, "Truckers Ltd");

            //check company is not in consignment form
            Click(The.Top, "Pre-Advice");
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            AtRow(The.Top).Column("Edit").Click("Edit");
            Click("Save and Add/Amend Consignments");
            Click("New Consignment");
            Type("Ltd");
            Click(What.Contains, "Imports Ltd");
            ClickField("Partner name");
            Type("Ltd");
            ExpectNo(What.Contains, "Truckers Ltd");
        }
    }
}