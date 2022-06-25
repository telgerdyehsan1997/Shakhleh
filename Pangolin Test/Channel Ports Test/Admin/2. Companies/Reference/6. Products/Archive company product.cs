using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveCompanyProduct : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsProduct_IPad>();

            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader(That.Contains, "Shipment");
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");

            NearLabel("Archived").ClickLabel("Active");
            Click("Search");
            WaitToSeeHeader("Products");
            // archive 
            AtRow("iPad").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            ExpectNoRow("iPad");
        }
    }
}