using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ImportCommodityCode_CheckEUQuotaValidation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Settings
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Commodity Code Imports");
            ExpectHeader("Commodity Code Imports");

            //Imports the Commodity Code
            ClickLink("New Import");
            ExpectHeader("Upload Commodity Codes");
            Set("Choose file").To("EuQuotaNumberValidation.csv");
            ClickButton("Save");
            ExpectHeader("Commodity Code Imports");

            //Run the background task to Upload Commodity Code
            AtRow("Download").Column("Import Status").Expect("Pending");
            CheckBackgroundTasks();
            AtRow("Run Commodity Code Import Service").ClickLink("Execute");
            WaitForNewPage();
            Goto("/");

            //Wait for Commodity Codes to finish upload and then check Commodity Codes
            ClickLink("Settings");
            ExpectHeader("Users");
            ClickLink("Commodity Code Imports");
            AtRow("Download").Column("Import status").Expect("Partial Success");
            AtRow("Download").Column("Errors").ClickLink("Errors");

            //Checks the import errors that have occured
            AtRow("7").Column("Error reason").Expect("EU Quote must be 6 numeric");
            AtRow("8").Column("Error reason").Expect("EU Quote must be 6 numeric");
        }
    }
}