using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_AddConsignmentToNCTSShipment_OutOfUkWithJustOneAuthorisedLocation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddCompanyWithOneAuthorisedLocationsBeta, AddCompanyUserForBetaJenny>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect("Beta Ltd - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            System.Threading.Thread.Sleep(1000);
            Click("Beta Ltd - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            ClickField("Primary contact");
            Expect("JENNY SMITH");
            System.Threading.Thread.Sleep(1000);
            Click("JENNY SMITH");

            System.Threading.Thread.Sleep(500);

            AtLabel("Use authorised location").ClickLabel("Yes");
            AtLabel("Authorised location").Expect("WAREHOUSE 1");
        }
    }
}
