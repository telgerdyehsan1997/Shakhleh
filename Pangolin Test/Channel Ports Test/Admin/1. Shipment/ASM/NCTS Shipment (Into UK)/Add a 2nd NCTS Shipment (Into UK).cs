using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddA2ndNCTSShipment_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAuthorisedLocationWarehouse1>();
            LoginAs<ChannelPortsAdmin>();

            AssumeDate("02/07/2019");

            Goto("/");

            //Click("NCTS Shipments Into UK");

            //ExpectHeader("NCTS Shipments Into UK");

            //Click("New NCTS Shipment Into UK");

            //ExpectHeader("NCTS Shipments Into UK Details");

            //Set("NCTS MRN").To("22AGZXYHFGD9876543");

            //Set("authorised location").To("ware");
            //Click("Warehouse 1");
            //Set("Entry number").To("ZYXV9876");
            //Set("Vehicle number").To("123");

            //Click("Save and Transmit");

            //ExpectHeader("NCTS Shipments Into UK");

          //  AtRow("22AGZXYHFGD9876543").Column("Date / time of arrival").Expect(""); //info is coming from another source. 
          //  AtRow("22AGZXYHFGD9876543").Column("Date / time of discharge").Expect(""); //info is coming from another source.
            //AtRow("22AGZXYHFGD9876543").Column("Progress").Expect("Transmitted");
          //  AtRow("22AGZXYHFGD9876543").Column("View error").Expect("");
        }
    }
}