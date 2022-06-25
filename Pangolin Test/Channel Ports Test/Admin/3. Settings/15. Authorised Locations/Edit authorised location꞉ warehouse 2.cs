using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditAuthorisedLocationWarehouse2 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAuthorisedLocationWarehouse2>();

            LoginAs<ChannelPortsAdmin>();

            Click("Settings");

            Click("Authorised Locations");

            ExpectHeader(That.Contains, "Authorised Locations");

            AtRow("Warehouse 2").Column("Edit").ClickLink();

            ExpectHeader("Authorised Location Details");

            Set("Location name").To("Depot 3");
            Set("Customs identity").To("Custom identity 3");
            Set("NCTS code").To("");
            ClickHeader("Authorised Location Details");
            ClickField("NCTS code");
            System.Threading.Thread.Sleep(1000);
            Expect("DO987654");
            System.Threading.Thread.Sleep(1000);
            Click("DO987654");
            Set("Authorisation number").To("3003");

            Click("Save");

            AtRow("Depot 3").Column("Location name").Expect("Depot 3");
            AtRow("Depot 3").Column("Customs identity").Expect("Custom identity 3");
            AtRow("Depot 3").Column("NCTS code").Expect("DO987654");
            AtRow("Depot 3").Column("Authorisation number").Expect("3003");
        }
    }
}