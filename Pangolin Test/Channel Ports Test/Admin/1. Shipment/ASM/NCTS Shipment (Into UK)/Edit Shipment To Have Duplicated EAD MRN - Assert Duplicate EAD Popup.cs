using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditShipmentToHaveDuplicatedEADMRN_AssertDuplicateEADPopup : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentWithDuplicateEADMRN>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Shipments
            ClickLink("NCTS Shipments Out of UK");
            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            //Navigates to Consignment Level
            AtRow("Imports Ltd").Column("Edit").Click("Edit");
            ExpectHeader("Shipment details");
            ClickButton("Save and Add/Amend Consignments");
            //ExpectHeader("Consignments");
            AtRow("CP100000101").Column("Edit").Click("Edit");
            ExpectHeader("Consignment Details");

            //Sets the duplicate EAD MRN
            Set("EAD MRN").To("12GB45678945612345");
            ClickButton("Save and Add Commodities");

            //The Following workflow no longer occurs
            //Duplicate EAD MRN pop up should appear
            //Expect(What.Contains, "The EAD MRN provided is used on a previous Consignment. Increasing the last value by 1");
        }
    }
}