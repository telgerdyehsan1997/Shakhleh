using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class DefaultDeclarantCanBeUsedInConsignment : UITest
    {
        [TestProperty("Sprint", "2")]
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {

            // these fields no longer show on the customer side of shipment details


            Run<JohnSmithAddsShipmentForTruckersLtd,AddCompanyAlpha>();
            LoginAs<JohnSmithCustomer>();
            Click("New Shipment");
            WaitToSeeHeader(That.Contains, "Shipment details");
            Click(What.Contains, "Nothing selected");
            Type("Jim");
            Press(Keys.Enter);
            ClickHeader();
            NearLabel("Out of UK").ClickLabel("Into UK");
            Set("Vehicle number").To("007");
            Click("Save and Add/Amend Consignments");
            Click("New Consignment");

            // check DD can be used
            ClickField("UK Trader");
            Type("Ltd");
            Expect(What.Contains, "Worldwide logistics ltd");
            ExpectNo(What.Contains, "Alpha Ltd");
            Press(Keys.Tab);
            ClickField("Partner name");
            Type("Ltd");
            Expect(What.Contains, "Worldwide logistics ltd");
            ExpectNo(What.Contains, "Alpha Ltd");
            Press(Keys.Tab);
            ClickField("Declarant");
            Type("Ltd");
            Expect(What.Contains, "Worldwide logistics ltd");
            ExpectNo(What.Contains, "Alpha Ltd");

            // change the DD to Alpha
            LoginAs<ChannelPortsAdmin>();
            Click(The.Top, "Settings");
            Click("Global Settings");
            ClickField("Default declarant");
            Set("Default declarant").To("");
            Type("Aplha Ltd");
            System.Threading.Thread.Sleep(500);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Click("Save");

            // reflected in customer
            LoginAs<JohnSmithCustomer>();
            Click("New Shipment");
            WaitToSeeHeader("Shipment details");
            Click(What.Contains, "Nothing selected");
            Type("Jim");
            Press(Keys.Enter);
            ClickHeader();
            NearLabel("Out of UK").ClickLabel("Into UK");
            Set("Vehicle number").To("008");
            Click("Save and Add/Amend Consignments");
            Click("New Consignment");

            // check DD can be used
            WaitToSeeHeader(That.Contains, "Consignment Details");
            Set("UK trader").To("");
            ClickField("UK Trader");
            Type("Ltd");
            ExpectNo(What.Contains, "Worldwide logistics ltd");
            Expect(What.Contains, "Alpha Ltd");
            Press(Keys.Tab);
            ClickField("Partner name");
            Type("Ltd");
            ExpectNo(What.Contains, "Worldwide logistics ltd");
            Expect(What.Contains, "Alpha Ltd");
            Press(Keys.Tab);
            Set("Declarant").To("");
            ClickField("Declarant");
            Type("Ltd");
            ExpectNo(What.Contains, "Worldwide Logistics Ltd");
            Expect(What.Contains, "Alpha");
        }
    }
}
