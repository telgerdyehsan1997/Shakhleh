using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCannotUseURLToAccessCommoditiesList : UITest
    {
        [TestProperty("Sprint", "2")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JohnSmithAddsCommodityToConsignment>();

            //go to commodity details page
            LoginAs<ChannelPortsAdmin>();
            WaitToSeeHeader("Shipments");
            Set("Date").To("01/01/1990");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow(That.Contains, "R0219000001").Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            Click("Save and Add/Amend Consignments");
            ExpectHeader(That.Contains, "Consignments");
            AtRow(That.Contains, "R0219000001").Column("Commodities").ClickLink("1");
            //Expect(What.Contains, "R0219000001 - Commodities");
            CopyUrl();

            //submit shipment
            LoginAs<ChannelPortsAdmin>();
            ExpectHeader("Shipments");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");
            AtRow(That.Contains, "R0219000001").Click("Edit");
            Click(What.Contains, "Save");
            Click("1");
            WaitForNewPage();

            //try to access add commodity page via url
            GotoCopiedUrl();
            ExpectHeader(That.Contains, "Consignments - Into UK");
        }
    }
}