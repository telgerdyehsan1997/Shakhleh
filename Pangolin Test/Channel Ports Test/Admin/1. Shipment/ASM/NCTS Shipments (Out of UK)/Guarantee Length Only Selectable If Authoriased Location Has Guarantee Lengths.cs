using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class GuaranteeLengthOnlySelectableIfAuthoriasedLocationHasGuaranteeLengths : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AddCompanyWithAuthorisedLocationsBeta, AddGuaranteeLength10DaysForWareHouse1>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");

            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            Set("Company name").To("Beta");
            Click(What.Contains, "Beta");

            ExpectNo("Guarantee length");
            ClickLabel("Use authorised location");

            ClickField("Select authorised location");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Warehouse 1");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Warehouse 1");
            Expect("Guarantee length");

            Set("Select authorised location").To("");
            ClickField("Select authorised location");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Warehouse 2");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Warehouse 2");
            ExpectNo("Guarantee lengths");
        }
    }
}
