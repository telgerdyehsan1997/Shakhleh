using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SearchAdminUndischargedNCTS : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddUndischargedNCTS_FillingForm_Microsoft, AddUndischargedNCTS_FillingForm_Amazon>();
            LoginAs<Undischarged_ChannelPortsAdmin>();


            //Testing CustomsPro Number Field
            Set("CustomsPro Number").To("CP100009902");
            Click("Search");

            ExpectRow("CP100009902");
            ExpectNoRow("CP100009901");

            Set("CustomsPro Number").To("");
            Click("Search");
            //Testing MRN Number Field
            Set("MRN Number").To("12GB00000000009902");
            Click("Search");

            ExpectRow("12GB00000000009902");
            ExpectNoRow("12GB00000000009901");

            Set("MRN Number").To("12GB00000000009902");
            Click("Search");

            //Testing Customer Name Field
            Set("Customer Name").To("MICROSOFT");
            Click("Search");

            ExpectRow("MICROSOFT");
            ExpectNoRow("AMAZON");

            Set("Customer Name").To("");
            Click("Search");

            //Testing Customer Reference Number Field
            Set("Customer Reference Number").To("123");
            Click("Search");

            ExpectRow("123");
            ExpectNoRow("321");

            Set("Customer Reference Number").To("");
            Click("Search");

            //Testing Customer Account Number Field
            Set("Customer Account Number").To("A1234");
            Click("Search");

            ExpectRow("A1234");
            ExpectNoRow("B1234");

            Set("Customer Account Number").To("");
            Click("Search");

            //Testing Vehicle Number Field
            Set("Vehicle Number").To("t34");
            Click("Search");

            ExpectRow("T34");
            ExpectNoRow("T44");

            Set("Vehicle Number").To("");
            Click("Search");

            //Testing Vehicle Number Field
            Set("Trailer Number").To("t34");
            Click("Search");

            ExpectRow("T34");
            ExpectNoRow("T44");

            Set("Trailer Number").To("");
            Click("Search");

            //Testing Consignment Value Field
            Set("Consignment Value").To("25");
            Set("Consignment Value").To("75");
            Click("Search");

            ExpectRow("50");
            ExpectNoRow("100");

            Set("Consignment Value").To("");
            Click("Search");

            //Testing Date of Notification Field
            Set("Date of Notification").To("25");
            Set("Date of Notification").To("75");
            Click("Search");

            ExpectRow("50");
            ExpectNoRow("100");

            Set("Date of Notification").To("");
            Click("Search");
        }
    }
}