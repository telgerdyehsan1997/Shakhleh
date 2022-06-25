using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveSecondQuantityDescription_Litres : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveSecondQuantityDescription_Litres>();

            //login
            LoginAs<ChannelPortsAdmin>();
            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            Click("Second Quantity Descriptions");
            WaitToSeeHeader(That.Contains, "Second Quantity Descriptions");

            //check SQD is in list filtered by ALL
            ClickLabel("All");
            Click("Search");
            AtRow(That.Contains, "89").Column(That.Contains, "Quantity code").Expect("89");
            AtRow(That.Contains, "89").Column(That.Contains, "Description").Expect("Litres");
            AtRow(That.Contains, "89").Column(That.Contains, "Archive").Expect("Unarchive");

            //check SQD is in list filtered by ACTIVE
            ClickLabel("Active");
            Click("Search");
            ExpectNoRow("89");

            //check SQD is NOT in list filtered by ARCHIVED
            ClickLabel("Archived");
            Click("Search");
            ExpectRow(That.Contains, "89");

            //unarchive item
            AtRow(That.Contains, "89").Column(That.Contains, "Archive").Click("Unarchive");
            ExpectHeader("Unarchive");
            ClickButton("Unarchive");
            WaitToSeeHeader(That.Contains, "Second Quantity Descriptions");
            ClickLabel("Active");
            ClickButton("Search");
            ExpectRow("89");
        }
    }
}