using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAuthorisedLocationWarehouse2 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewTransitOffice>();

            LoginAs<ChannelPortsAdmin>();

            Click("Settings");

            Click("Authorised Locations");

            ExpectHeader(That.Contains, "Authorised Locations");
            Click("New Authorised Location");

            ExpectHeader(That.Contains, "Authorised Location Details");

            Set("Location name").To("Warehouse 2");
            Set("Customs identity").To("Customs identity 2");
            At("NCTS code").ClickField();
            Type("DO9876");
            Click("DO987654");
            Set("Authorisation number").To("1001");
            Click("Save");

            AtRow("Warehouse 2").Column("Location name").Expect("Warehouse 2");
            AtRow("Warehouse 2").Column("Customs identity").Expect("Customs identity 2");
            AtRow("Warehouse 2").Column("NCTS code").Expect("DO987654");
            AtRow("Warehouse 2").Column("Authorisation number").Expect("1001");
        }
    }
}