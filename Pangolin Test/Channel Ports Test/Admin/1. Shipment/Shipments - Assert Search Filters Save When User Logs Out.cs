using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Shipments_AssertSearchFiltersSaveWhenUserLogsOut : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321";

            Run<AddShipmentIntoUKASMAccepted>();
            LoginAs<ChannelPortsAdmin>();

            //Sets the search filters
            Set("Tracking number").To("R0721000001");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);
            Set("Trailer number").To("1234567");
            Set("Date created").To("01/07/1999");
            Set("Expected date of arrival/departure").To("19/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            Set("Customer Reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Progress").To("Ready to Transmit");
            ClickButton("Search");
            ExpectRow("R0721000001");

            //Signs out of Accounts and then signs in again
            ClickLink("Logout");
            ExpectHeader("Please Login");
            Set("Email").To("admin@uat.co");
            Set("Password").To("test");
            ClickButton("Login");

            //Asserts that filters have saved
            ExpectHeader("Shipments");
            AtField("Tracking number").Expect("R0721000001");
            AtField("Port of arrival/departure").Expect("Portsmouth");
            AtField("Company name").Expect("GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            AtField("Trailer number").Expect("1234567");
            AtLabel("Date created").Expect("01/07/1999");
            AtLabel("Expected date of arrival/departure").Expect("19/01/1999");
            AtField(The.Top, "to").Expect("");
            AtField(The.Bottom, "to").Expect("");
            AtField("Customer Reference").Expect("R123");
            AtField("Vehicle number").Expect("RAF123");
            AtLabel("Progress").Expect("Ready to Transmit");
        }
    }
}