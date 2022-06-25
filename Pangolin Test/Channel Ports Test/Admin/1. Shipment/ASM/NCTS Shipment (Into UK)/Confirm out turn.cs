using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ConfirmOutTurn : UITest
    {
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipment_IntoUK>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Into UK");
            ExpectHeader("NCTS Shipments Into UK");

            AtRow("99MBABCDEFG1234567").Column("Confirm Out Turn").ClickButton();
            ExpectHeader("Confirm Out Turn");
            Set("Number of packages").To("10");
            Set("Total gross mass (kg)").To("100");
            Set("Transport ID").To("1");
            Set("Transport country").To("24");
            Click("Save");

            ExpectHeader("NCTS Shipments (Into UK)");

            //Progress column currently not implemented in system 05/05/2020
            AtRow("99MBABCDEFG1234567").Column("Progress").Expect("Out Turn");



        }
    }
}