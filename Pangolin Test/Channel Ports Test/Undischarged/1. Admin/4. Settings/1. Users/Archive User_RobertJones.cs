using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveUser_RobertJones : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddNewUndischargedUser_RobertJones>();

            // ----------------------------------------------

            LoginAs<Undischarged_ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitToSeeHeader("Users");

            // ----------------------------------------------

            //archive user - cancel
            AtRow(That.Contains, "robert.jones@uat.co").Column(That.Contains, "Archive").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            ClickButton("Cancel");
            // ----------------------------------------------

            //archive user - confirm
            AtRow(That.Contains, "robert.jones@uat.co").Column(That.Contains, "Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            WaitToSeeHeader(That.Contains, "Users");

        }
    }
}