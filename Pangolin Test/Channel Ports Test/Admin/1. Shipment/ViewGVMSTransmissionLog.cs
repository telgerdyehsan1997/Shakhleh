using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ViewGVMSTransmissionLog : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION";

            Run<AddCommodityForMajimaConstruction>();
            LoginAs<ChannelPortsAdmin>();

            //Adds GVMS to the Company
            Click("Companies");
            ExpectHeader("Companies");
            AtRow(companyName).Column("Edit").Click("Edit");
            ExpectHeader("Record Details");
            Set("GVMS").To("Sometimes");
            Click("Save");
            ExpectHeader("Companies");

            //Navigates to Shipments and enables GVMS 
            Click("Shipments");
            ExpectHeader("Shipments");
            AtRow(companyName).Column("Edit").Click("Edit");
            ExpectHeader("Shipment Details");
            //AtLabel("GVMS").ClickCheckbox();
            Click("Save and Add/Amend Consignments");
            ExpectHeader(That.Contains, "Consignments");

            //Navigates to the GVMS Transmission log
            Click("GVMS Transmission Log");
            ExpectHeader("GVMS Transmission Log");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Click("Search");
            ExpectRow(companyName);

            //Asserts the new column
            AtRow(companyName).ExpectColumn("GVMS status");
        }
    }
}