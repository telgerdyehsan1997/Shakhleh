using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckingInvoiceCurrencyDataSource : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentForWWL>();

            AtRow("T0721000001").Column("Edit").Click("Edit");
            ClickButton("Save and Add/Amend Consignments");

            Set("Invoice currency").To("Eurozone - EUR");
            System.Threading.Thread.Sleep(1000);

            Set("Invoice currency").To("Great");
            System.Threading.Thread.Sleep(1000);

            Set("Invoice currency").To("USA");
            System.Threading.Thread.Sleep(2000);
            Click("Cancel");
            ClickLink("Shipments");

        }
    }
}