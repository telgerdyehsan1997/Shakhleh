using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCPC : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCPC_12345, CreateNewCPC_54321>();
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            WaitToSeeHeader("Users");
            Click("CPC");
            WaitToSeeHeader("CPC");

            // add cancel
            Click("New CPC");
            ExpectHeader("CPC details");
            Click("Cancel");
            ExpectHeader("CPC");

            // edit cancel
            AtRow("CP12345").Click("Edit");
            ExpectHeader("CPC details");
            Set("CPC number").To("Edited1");
            Click("Cancel");
            ExpectRow("CP12345");

            // edit
            AtRow("CP12345").Click("Edit");
            ExpectHeader("CPC details");
            Set("CPC number").To("Edited1");
            Click("Save");
            ExpectRow("Edited1");

            // archive cancel
            ClickLabel("All");
            Click("Search");
            AtRow("CP54321").Click("Archive");
            ExpectHeader("Archive");
            Click("Cancel");
            AtRow("CP54321").Column("Archive").Expect("Archive");

            // archive cancel
            AtRow("CP54321").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");
            AtRow("CP54321").Column("Archive").Expect("Unarchive");

            // search exclusion: find
            Set("Find").To("Nothere");
            Click("Search");
            ExpectNoRow("Edited1");
            ExpectNoRow("CP54321");

            // search inclusion: find
            Set("Find").To("Edited");
            Click("Search");
            ExpectRow("Edited1");
            ExpectNoRow("CP54321");

            // search exclusion: Status
            Set("Find").To("");
            ClickLabel("Archived");
            Click("Search");
            ExpectNoRow("Edited1");
            ExpectRow("CP54321");

            // search inclusion: Status
            ClickLabel("Active");
            Click("Search");
            ExpectRow("Edited1");
            ExpectNoRow("CP54321");
        }
    }
}
