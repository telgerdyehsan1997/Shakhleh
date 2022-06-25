using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveAuthorisedLocationWarehouse2 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAuthorisedLocationWarehouse2>();

            LoginAs<ChannelPortsAdmin>();

            Click("Settings");

            Click("Authorised Locations");

            ExpectHeader(That.Contains, "Authorised Locations");

            AtRow("Warehouse 2").Column("Archive").Click("Archive");

            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");

            ExpectNo("Warehouse 2");
            ExpectNo("Customs identity 2");
            ExpectNo("2001");


            ClickLabel("Archived");
            Click("Search");

            Expect("Warehouse 2");
            Expect("Customs identity 2");
            Expect("1001");
        }
    }
}