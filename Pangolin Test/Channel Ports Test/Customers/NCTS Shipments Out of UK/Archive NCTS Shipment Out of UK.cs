using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchiveNCTSShipmentOutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNewNCTSShipmentOutOfUKWithGroupNotifications>();

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");

            AtRow("1000000").Column("Archive").Click("Archive");

            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            Set("Please Explain Why").To("Archived Shipment");
            ClickButton("Archive");

            ExpectNo("100000");

        }
    }
}