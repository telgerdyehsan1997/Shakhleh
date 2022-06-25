using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSharp.Framework;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewShipmentForTruckersLtd : UITest
    {
        [TestProperty("Sprint", "1")]
        [TestProperty("AMP", "112781")]
        [TestProperty("Sprint", "6")]
        [TestProperty("AMP", "126866")]
        [TestCategory("File Upload To Be Added")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddRoutePortsmouthToCalais, AdminAddsCompanyTruckersLtd, CreateNewCompanyUser_JohnSmith, AddNewContactGroup_Import, OfficeOfTransitES>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            ClickLink("New Shipment");

            WaitToSeeHeader("Shipment Details");

            Type("Truckers Ltd");
            Expect(What.Contains, "Truckers");
            Click(What.Contains, "TRUCKERS");
            ClickLabel("Out of UK");
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
            Set("Customer Reference").To("Testerson");
            ClickLabel("Into UK");
            ClickField("Route");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "CALAIS to Portsmouth");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "CALAIS to Portsmouth");

            Set("Vehicle number").To("T37");
            Set("Trailer number").To("T87");
            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("25/12/2022");
            Click(What.Contains, "Save");

            /*ClickField("Office of Destination");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "GB DOVER/FOLKESTONE EUROTUNNEL FREIGHT GB000060 UNITED KINGDOM"); */

            /*ClickField("Port of arrival");
            Type("Por");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, "Portsmouth");*/
            //ClickButton(That.Contains, "Save");

            ExpectHeader(That.Contains, "Consignment Details");
            Click("Shipments");
            WaitToSeeHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");

            Click("Search");

            ExpectHeader("Shipments");

        }
    }
}
