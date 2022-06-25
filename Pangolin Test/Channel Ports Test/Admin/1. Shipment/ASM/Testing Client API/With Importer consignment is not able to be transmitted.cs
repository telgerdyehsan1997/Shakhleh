using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class WithImporterConsignmentIsNotAbleToBeTransmitted : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<ChangeConsignmentToWithImporter>();

            LoginAs<ChannelPortsAdmin>();

            //add shipment
            ExpectHeader("Shipments");
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");

            Type("Import");
            System.Threading.Thread.Sleep(2000);
            Click("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");

            Set("Customer Reference").To("B2356");
            Set("Vehicle number").To("45698");
            Set("Trailer number").To("456465");
            Set("Port of arrival").To("Portsmouth");

            Click(What.Contains, "Save");
            System.Threading.Thread.Sleep(1000);
            Type("16/12/2020");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Save");

            //add consignment
            ExpectHeader("Consignment Details");

            AtField("UK trader");
            Type("Import");
            System.Threading.Thread.Sleep(2000);
            Click("Imports Ltd - Rome - AG2 YGD - IL859098859098 - 6234517");


            Set("Partner name").To("Channel Ports - Hythe - CT21 4BL - GB683470514001");
            System.Threading.Thread.Sleep(1000);

            Set("Declarant").To("Channel Ports - Hythe - CT21 4BL - GB683470514001");
            Set("Total packages").To("2");
            Set("Total gross weight").To("1000");
            Set("Total net weight").To("900");
            Set("Invoice number").To("456789");
            Set("Invoice currency").To("GREAT BRITAIN - GBP");
            Set("Total value").To("1000");
            Set("Terms of Sale").To("EXW");
            Click("Save and Add Commodities");

            //add commodities

            ClickLink("New Commodity");


            ClickField("Product code");
            Type("IPOD 32");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Gross weight").To("450");
            Set("Net weight").To("400");
            Set("Second quantity").To("10");
            Set("Third quantity").To("10");
            Set("Value").To("450");
            Set("Number of packages for this commodity code (if known)").To("1");
            Set("Country of origin").To("ES - Spain");
            Click("Save");

            ExpectRow("ABS00003");

            ClickLink("New Commodity");

            ClickField("Product code");
            Type("IPOD 64");
            System.Threading.Thread.Sleep(1000);
            Press(Keys.ArrowDown);
            Press(Keys.Enter);
            Set("Gross weight").To("550");
            Set("Net weight").To("500");
            Set("Second quantity").To("10");
            Set("Third quantity").To("10");
            Set("Value").To("550");
            Set("Number of packages for this commodity code (if known)").To("1");
            Set("Country of origin").To("ES - Spain");
            Click("Save");

            ExpectRow("ABS00003");
            ExpectRow("ABS00004");

            ClickButton("Complete");

        }
    }
}