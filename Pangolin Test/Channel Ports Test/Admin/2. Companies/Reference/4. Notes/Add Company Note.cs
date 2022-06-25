using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddCompanyNote : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            AssumeDate("19/02/2019");
            AssumeTime("12:00");
            Goto("/");

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow("Truckers Ltd").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Notes");
            WaitToSeeHeader("Notes");
            Click("New Note");
            WaitToSeeHeader("Note Details");

            // ----------------------------------------------

            // Create new note
            Set("Note").To("This is a test note to test the system.");
            Click("Save");

            // ----------------------------------------------

            // Expect
            ExpectRow("Geeks Admin");
            AtRow("Geeks Admin").Column("Date/time").Expect("19/02/2019 12:00");
            AtRow("Geeks Admin").Column("Note").Expect("This is a test note to test the system.");


        }
    }
}