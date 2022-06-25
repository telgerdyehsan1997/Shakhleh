using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JohnSmithAddsShipmentForTruckersLtd : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount, AdminChangesTruckersLtdCompanyType, AddRouteSouthamptonAndValencia>();
            LoginAs<JohnSmithCustomer>();
            AssumeDate("1/1/2019");
            Goto("/");
            WaitToSeeHeader(That.Contains, "Shipments Into UK");

            //create new Shipment
            Click("New Shipment");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Southampton to VALENCIA");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Southampton to VALENCIA");

            WaitToSeeHeader(That.Contains, "Shipment Details");
            RightOf("Primary contact").Click("AddContact");
            WaitForPopup();
            WaitToSeeHeader("New Contact");
            Set("First name").To("Jim");
            Set("Last name").To("Stevens");
            Set("Email address").To("jimstevens123@uat.co");
            Set("Telephone number").To("1234");
            Set("Mobile number").To("5678");
            Click("Save");
            WaitToSeeHeader(That.Contains, "Shipment Details");
            Set("Customer Reference").To("55555");
            ClickLabel("Into UK");
            Set("Vehicle number").To("89A");
            Set("Trailer number").To("6514");

            /*ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB Dover/Folkestone Eurotunnel Freight GB000060 United Kingdom"); */
            /*ClickField("Port of arrival");
            Type("Blackp");
            Click("Blackpool");*/
            //Set("Choose file").To("Example.png");
            //BelowText("Border crossing").AboveText("Second border crossing").ClickField();
            //Click("United Kingdom"); Not needed for Into UK
            Click("Save and Add/Amend Consignments");
            ClickButton(That.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("01/02/2022");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            Click("Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");

            //assert list row
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0119000001").Column("Tracking number").Expect("R0119000001");
            //AtRow(That.Contains, "R0719000001").Column("Date").Expect("01/07/2019");
            AtRow(That.Contains, "R0119000001").Column("Expected date of arrival/departure").Expect("01/02/2022");
            AtRow(That.Contains, "R0119000001").Column("Customer Reference").Expect("55555");
            AtRow(That.Contains, "R0119000001").Column("Company name").Expect(What.Contains, "Truckers Ltd");
            AtRow(That.Contains, "R0119000001").Column("Vehicle number").Expect("89A");
            AtRow(That.Contains, "R0119000001").Column("Trailer number").Expect("6514");
            AtRow(That.Contains, "R0119000001").Column("Progress").Expect("Draft");
            //AtRow(That.Contains, "R0119000001").Column("Weights mismatch").ExpectNoTick();
        }
    }
}
