using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [Ignore]
    [TestClass]
    public class ArchivedContactGroupNotVisibleInAdviceForm : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewContactGroup_Import>();

            //check contact group in form
            LoginAs<ChannelPortsAdmin>();

            ClickField("Company name");
            Type("Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Truckers");
            ClickLabel("Group");
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect("Import");

            //archive
            Click(The.Top, "Companies");
            WaitForNewPage();
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").ClickLink();
            Click("Contact Groups");
            WaitToSeeHeader(That.Contains, "Contact Groups");
            AtRow(That.Contains, "Import").Column("Archive").Click("Archive");
            WaitToSee("Are you sure you want to archive this Contact Group?");
            Click("OK");

            //check contact group not in form
            Click(The.Top, "Pre-Advice");
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            Click("New Advice");
            WaitToSeeHeader(That.Contains, "Advice Details");
            ClickField("Company name");
            Type("Ltd");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Truckers");
            ClickLabel("Group");
            Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            ExpectNo("Import");
        }
    }
}