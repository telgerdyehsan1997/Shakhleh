using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminAddsTwoShipments : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddRoutePortsmouthToCalais, AdminAddsCompanyTruckersLtd, CreateNewCompanyUser_JohnSmith, AddNewContactGroup_Import>();
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Shipments");
            WaitToSeeHeader("Shipments");
            ClickLink("New Shipment");

            WaitToSeeHeader("Shipment Details");

            Type("Truckers Ltd");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "Truckers Ltd");
            ClickLabel("Into UK");
            AtLabel("Company type").Expect("Customer");
            ClickField("Primary contact");
            //Type("Jo");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            ClickLabel("Group");
            Above("Customer Reference").Click("---Select---");
            WaitToSeeText("Import");
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Customer Reference").To("RT564744");
            ClickLabel("Into UK");

            Set("Vehicle number").To("T37");
            Set("Trailer number").To("T87");
            System.Threading.Thread.Sleep(1000);
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Click("CALAIS to Portsmouth");


            // ClickButton(That.Contains, "Save and Add/Amend Consignments");
            ClickButton(That.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("01/07/2021");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            WaitToSeeHeader(That.Contains, "Consignment Details");
            Click("Shipments");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow(That.Contains, "R0120000001").Column("Date").Expect("01/01/2020");
            AtRow(That.Contains, "R0120000001").Column("Type").Expect("Into UK");
            AtRow(That.Contains, "R0120000001").Column("Expected date of arrival/departure").Expect("01/03/2020");
            AtRow(That.Contains, "R0120000001").Column("Port of arrival/departure").Expect("Portsmouth");
            AtRow(That.Contains, "R0120000001").Column("Customer Reference").Expect("RT564744");
            AtRow(That.Contains, "R0120000001").Column("Company name").Expect("Truckers Ltd");
            AtRow(That.Contains, "R0120000001").Column("Vehicle number").Expect("T37");
            AtRow(That.Contains, "R0120000001").Column("Trailer number").Expect("T87");
            AtRow(That.Contains, "R0120000001").Column("Progress").Expect("Draft");
            AtRow(That.Contains, "R0120000001").Column("Weights mismatch").ExpectNoTick();


            Click(The.Top, "Shipments");
            WaitToSeeHeader("Shipments");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");

            Type("Truckers Ltd");
            System.Threading.Thread.Sleep(2000);
            Click(What.Contains, "Truckers Ltd");
            ClickLabel("Into UK");
            AtLabel("Company type").Expect("Other");
            ClickField("Primary contact");
            //Type("Jo");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            ClickLabel("Group");
            Above("Customer Reference").Click("---Select---");
            WaitToSeeText("Import");
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Customer Reference").To("RT564744");
            ClickLabel("Into UK");

            Set("Vehicle number").To("T37");
            Set("Trailer number").To("T87");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Click("CALAIS to Portsmouth");


            // ClickButton(That.Contains, "Save and Add/Amend Consignments");
            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("01/03/2020");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");
        }
    }
}
