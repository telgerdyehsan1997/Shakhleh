using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManagePorts : UITest
    {
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126987")]
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewTransitOffice, AddPortGoole>();
            // ------------------------------
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");

            Click("Ports");

            WaitToSeeHeader("Ports");

            // Add - Cancel
            Click("New Port");
            WaitToSeeHeader("Port Details");
            Click("Cancel");

            // Edit - Cancel
            AtRow("Portsmouth").Click("Edit");
            WaitToSeeHeader("Port Details");
            Set("Port name").To("Dover");
            Set("Port code").To("DOV");
            Set("NCTS code").To("United Kingdom - GB000060");
            Click(What.Contains, "United Kingdom - GB000060");
            AtLabel("Non-UK").ClickLabel("Yes");
            Click("Cancel");

            ExpectNoRow("Dover");
            ExpectRow("Portsmouth");

            // Edit - Save
            AtRow("Portsmouth").Click("Edit");
            WaitToSeeHeader("Port Details");
            AtLabel("Country").Click(What.Contains, "---Select---");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "United Kingdom");
            AtLabel("Into UK Type").ClickLabel("GVMS");
            Set("Port name").To("Dover");
            Set("Port code").To("DOV");
            Set("NCTS code").To("United Kingdom - GB000060");
            Click(What.Contains, "United Kingdom - GB000060");
            Click("Save");

            ExpectNoRow("Portsmouth");
            ExpectRow("Dover");

            // Archive - Cancel
            AtRow("Dover").Click("Archive");
            ExpectHeader("Archive");
            Expect("Please Explain Why");
            Click("Cancel");
            AtRow("Dover").Column("Archive").Expect("Archive");

            // Archive - OK
            AtRow("Dover").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectNoRow("Dover");
            ClickLabel("Archived");
            Click("Search");
            ExpectRow("Dover");
            AtRow("Dover").Column("Archive").Expect("Unarchive");

            // Find
            Set("Find").To("Dove");
            Click("Search");
            ExpectRow("Dover");
        }
    }
}
