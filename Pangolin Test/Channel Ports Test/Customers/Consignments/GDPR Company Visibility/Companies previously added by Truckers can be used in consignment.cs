using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CompaniesPreviouslyAddedByTruckersCanBeUsedInConsignment : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112152")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddConsignmentWith3AddedCompanies>();

            LoginAs<JohnSmithCustomer>();
            Click("New Shipment");
            WaitToSeeHeader(That.Contains, "Shipment Details");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "JIM STEVENS");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "JIM STEVENS");
            Set("Customer Reference").To("123455");
            Near("Out of UK").ClickLabel("Into UK");
            Set("Vehicle number").To("187");
            Set("Expected date of arrival").To("01/01/2023");

            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "VALENCIA to Southampton");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "VALENCIA to Southampton");

            Click("Save and Add/Amend Consignments");

            //// check companies appear in the 3 fields
            //Set("UK Trader").To("TRUCKERS LTD - WORCESTER - WR5 3DA - GB683470514001 - 7654321");

            //Expect(What.Contains, "Declarant - London - W1J 9HS - GB111222333778");
            //Expect(What.Contains, "Partner - London - W1J 9HS - GB111222333776");
            //Expect(What.Contains, "UK Trader - London - W1J 9HS - GB111222333777");
            //Click(What.Contains, "UK Trader - London - W1J 9HS - GB111222333777");

            Set("UK trader").To("");
            ClickHeader("Consignment Details");
            ClickField("UK trader");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ADDED UK TRADER - LONDON - W1J 9HS - GB683470514001 - 9876549");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ADDED UK TRADER - LONDON - W1J 9HS - GB683470514001 - 9876549");

            Set("Partner name").To("");
            ClickHeader("Consignment Details");
            ClickField(That.Contains, "Partner name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ADDED PARTNER - PARIS - W1J 9HS - GB683470514001");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ADDED PARTNER - PARIS - W1J 9HS - GB683470514001");

            Set("Declarant").To("");
            ClickHeader("Consignment Details");
            ClickField("Declarant");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "ADDED DECLARANT - LONDON - W1J 9HS - GB683470514001 - 9876542");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "ADDED DECLARANT - LONDON - W1J 9HS - GB683470514001 - 9876542");
        }
    }
}