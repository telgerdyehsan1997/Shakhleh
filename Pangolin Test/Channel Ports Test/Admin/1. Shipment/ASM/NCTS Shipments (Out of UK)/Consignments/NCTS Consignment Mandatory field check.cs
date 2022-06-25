using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NCTSConsignmentMandatoryFieldCheck : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddNewNCTSShipments_OutOfUK>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("1000000").Column("Edit").ClickLink();

            ExpectHeader("Shipment Details");

            Click("Save and Add/Amend consignments");

            ExpectHeader("Consignment Details");
            Set(That.Contains, "EAD MRN").To("123456");
            Click("Search");

            Set("EAD MRN").To("");

            var MandatoryFieldLabels = new string[] {
                "EAD MRN",
                "UK trader",
                "Partner name",
                "Country of destination",
                "Total packages",
                "Total gross weight",
                "Total net weight",
                "Invoice currency",
                "Total value"
            };

            Click("Save and Add Commodities");

            foreach (var MandatoryField in MandatoryFieldLabels)
            {
                Expect("The " + MandatoryField + " field is required.");
            }

        }
    }
}