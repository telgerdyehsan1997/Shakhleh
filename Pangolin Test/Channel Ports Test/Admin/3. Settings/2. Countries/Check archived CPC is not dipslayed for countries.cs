using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckArchivedCPCIsNotDipslayedForCountries : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ArchiveCPC>();
            LoginAs<ChannelPortsAdmin>();

            Click("Settings");
            Click("Countries");
            WaitToSeeHeader("Countries");
            Click("New Country");

            //check import CPC without preference autocomplete
            ClickField("Import CPC without preference");
            Type("CP1234");
            ExpectNo("CP12345");
            Expect("Not found");

            //check Export CPC without preference autocomplete
            ClickField("Export CPC without preference");
            Type("CP1234");
            ExpectNo("CP12345");
            Expect("Not found");

            //check import CPC with preference autocomplete
            AtLabel("Preference available").ClickLabel("Yes");
            ClickField("Import CPC with preference");
            Type("CP1234");
            ExpectNo("CP12345");
            Expect("Not found");

            //check export CPC with preference autocomplete
            ClickField("Export CPC with preference");
            Type("CP1234");
            ExpectNo("CP12345");
            Expect("Not found");
        }
    }
}