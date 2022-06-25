using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class UpdateCountryWithMFNCode_France : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Countries");
            Click("Countries");
            WaitToSeeHeader("Countries");
            ExpectRow("France");

            // ----------------------------------------------

            AtRow("France").Column("Country").ClickLink();
            WaitToSeeHeader("Country Details");

            ExpectNoField("MFN code 1");
            ExpectNoField("MFN code 2");
            ExpectNoField("MFN code 3");
            ExpectNoField("MFN code 4");
            ExpectNoField("MFN code 5");
            AtLabel("MFN").ClickLabel("Yes");
            ExpectField("MFN code 1");
            ExpectField("MFN code 2");
            ExpectField("MFN code 3");
            ExpectField("MFN code 4");
            ExpectField("MFN code 5");

            Click("Save");

            Expect(What.Contains, "The MFN code 1 field is required.");
            ExpectNo(What.Contains, "The MFN code 2 field is required.");
            ExpectNo(What.Contains, "The MFN code 3 field is required.");
            ExpectNo(What.Contains, "The MFN code 4 field is required.");
            ExpectNo(What.Contains, "The MFN code 5 field is required.");

            Set("MFN code 1").To("A.B");
            Set("MFN code 2").To("C.D");
            Set("MFN code 3").To("E.F");
            Set("MFN code 4").To("G.H");
            Set("MFN code 5").To("I.J");

            Click("Save");

            AtRow("France").Column("MFN code 1").Expect("A.B");
            AtRow("France").Column("MFN code 2").Expect("C.D");
            AtRow("France").Column("MFN code 3").Expect("E.F");
            AtRow("France").Column("MFN code 4").Expect("G.H");
            AtRow("France").Column("MFN code 5").Expect("I.J");
        }
    }
}