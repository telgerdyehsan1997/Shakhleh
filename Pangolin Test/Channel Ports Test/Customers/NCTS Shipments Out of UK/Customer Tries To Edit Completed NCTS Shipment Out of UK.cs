using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerTriesToEditCompletedNCTSShipmentOutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOutOfUKWithGroupNotifications>();

            //chnage status of this shipment in the database to completed

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");
            ExpectNoButton("Edit");
        }
    }
}