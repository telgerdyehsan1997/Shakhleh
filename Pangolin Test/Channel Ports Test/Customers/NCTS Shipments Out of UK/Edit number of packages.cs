using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditNumberOfPackages : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddEADMRNKnownConsignmentToNCTSOutOfUKShipment>();

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            AtRow("1000000").Column("Edit").ClickLink();

            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            Click("Edit");
            Click("Save and Add Commodities");

            Click("Edit");

            //check the edit form
            ExpectHeader(That.Contains, "Commodity Details");
            AtField("Commodity code").Expect("34545343453 - 14");
            AtLabel("Description of goods").Expect("iPod 32GB");
            AtLabel("Gross weight").Expect("5");
            AtLabel("Net weight").Expect("3");
            AtLabel("Currency").Expect("GBP");
            AtLabel("Value").Expect("500.50");
            AtLabel("Number of packages for this commodity code (if known)").ExpectValue("2");

            //edit no of packages
            Set("Number of packages for this commodity code (if known)").To("50");
            Click("Save");
            AtRow("34545343453 - 14").Column("Number of packages").Expect("50");
        }
    }
}