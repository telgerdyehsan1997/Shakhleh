using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageDeposits : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsConsignmentToShipmentForTruckersLtd>();

            //navigate
            LoginAs<JohnSmithCustomer>();
            WaitToSeeHeader("Shipments Into UK");
            Click("Deposits");


            //assert layout
            ExpectHeader("Deposits");
            BelowHeader("Deposits").ExpectLabel("Remaining Balance");
            BelowLabel("Remaining Balance").ExpectLabel("Pending Transaction Value");
            BelowLabel("Pending Transaction Value").ExpectLabel("Remaining Balance After Pending");
            BelowLabel("Remaining Balance After Pending").ExpectLabel("Tracking Number");
            BelowLabel("Tracking Number").ExpectLabel("Date Added");

            //assert details
            AtLabel("Remaining Balance").Expect("£0.00");
            RightOfLabel("Pending Transaction Value").Expect("£0.00");
            AtLabel("Remaining Balance After Pending").Expect("£0.00");
        }
    }
}