using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewDeposit_TruckersLTD_ArchiveDeposit : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Companies
            ClickLink("Companies");
            ExpectHeader("Companies");
            AtRow("TRUCKERS LTD").Column("Company name").ClickLink();
            ExpectHeader("TRUCKERS LTD");

            //Create new Deposit
            ClickLink("Deposits");
            ExpectHeader("Deposits");
            Click("New Deposit");
            ExpectHeader("Deposit Details");
            NearLabel("Withdrawal").ClickLabel("Deposit");
            //AtLabel("Transaction type").Choose("Deposit");
            ExpectLabel("Deposit in");
            Set("Date added").To("01/01/2023");
            Set("Deposit in").To("100");
            ClickButton("Save");
            AtLabel("Remaining Balance").Expect("£100.00");

            //Archive the deposit
            ExpectRow("£100.00");
            AtRow("£100.00").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archived Deposit");
            ClickButton("Archive");
            ExpectNoRow("£100.00");
            AtLabel("Remaining Balance").Expect("£0.00");
        }
    }
}