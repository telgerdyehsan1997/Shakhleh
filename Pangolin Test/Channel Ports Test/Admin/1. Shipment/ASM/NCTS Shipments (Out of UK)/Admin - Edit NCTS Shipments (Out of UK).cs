using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Admin_EditNCTSShipments_OutOfUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Admin_AddA2ndNCTSShipments_OutOfUK>();

            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            AtRow("1000000").Column("Edit").ClickLink();

            Set("Customer Reference").To("76543");
            Set("Vehicle number").To("586");
            Set("Trailer number").To("63432");
            Set("Expected date of departure").To("25/12/2022");

            Click("Save and Add/Amend Consignments");
            Click("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("1000000").Column("Expected date of departure").Expect("25/12/2022");
            AtRow("1000000").Column("Customer Reference").Expect("76543");
            AtRow("1000000").Column("Vehicle number").Expect("586");
            AtRow("1000000").Column("Trailer number").Expect("63432");
        }
    }
}