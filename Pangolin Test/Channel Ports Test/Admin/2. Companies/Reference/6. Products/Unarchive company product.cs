using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UnarchiveCompanyProduct : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveCompanyProduct>();


            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");

            ClickLabel("Archived");
            Click("Search");
            // archive 
            AtRow("iPad").Click("Unarchive");
            ExpectHeader("Unarchive");
            Set("Please Explain Why").To("Unarchive reason");
            ClickButton("Unarchive");
            ExpectNoRow("iPad");
            ClickLabel("Active");
            ClickButton("Search");
            ExpectRow("ABS12343");
        }
    }
}