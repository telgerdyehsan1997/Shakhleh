using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAuthorisedLocationWarehouse1 : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var nctsCode = "IT112345";

            Run<CreateNewTransitOffice, CreateNewOfficeOfTransit_Italy>();

            LoginAs<ChannelPortsAdmin>();

            Click("Settings");

            Click("Authorised Locations");

            ExpectHeader(That.Contains, "Authorised Locations");
            Click("New Authorised Location");

            ExpectHeader(That.Contains, "Authorised Location Details");

            Set("Location name").To("Warehouse 1");
            Set("Customs identity").To("Customs identity 1");
            ClickField("NCTS code");
            Click(What.Contains, nctsCode);
            Set("Authorisation number").To("1001");
            Click("Save");

            AtRow("Warehouse 1").Column("Location name").Expect("Warehouse 1");
            AtRow("Warehouse 1").Column("Customs identity").Expect("Customs identity 1");
            AtRow("Warehouse 1").Column("NCTS code").Expect(nctsCode);
            AtRow("Warehouse 1").Column("Authorisation number").Expect("1001");

        }
    }
}