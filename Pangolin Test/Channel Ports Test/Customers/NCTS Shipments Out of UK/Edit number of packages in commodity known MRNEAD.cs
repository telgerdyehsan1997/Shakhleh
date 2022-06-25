using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditNumberOfPackagesknownEADMRN : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddEADMRNKnownConsignmentToNCTSOutOfUKShipment>();

            LoginAs<JohnSmithCustomer>();

            Click("NCTS Shipments Out of UK");

            ExpectHeader("NCTS Shipments Out of UK");
            Set(The.Bottom, "to").To("25/12/2025");
            Click("Search");
            AtRow("1000000").Column("Edit").ClickLink();

            ExpectHeader("Shipment Details");
            Click("Save and Add/Amend Consignments");
            AtRow("CP100000001").Column("Commodities").ClickLink();

            //check the edit form
            AtRow("34545343453 - 14").Column("Edit").Click("Edit");
            ExpectHeader(That.Contains, "Commodity Details");
            AtLabel("Commodity code").Expect("34545343453 - 14");
            AtLabel("Description of goods").Expect("IPOD 32GB");
            AtLabel("Gross weight").Expect("5");
            AtLabel("Net weight").Expect("3");
            AtLabel("Currency").Expect("GBP");
            AtLabel("Value").Expect("500.50");
            AtLabel("Number of packages for this commodity code (if known)").ExpectValue("2");
            Click("Save");

            //edit no of packages
            AtRow("34545343453 - 14").Column("Edit").Click("Edit");
            Set("Number of packages for this commodity code (if known)").To("50");
            Click("Save");
            AtRow("34545343453 - 14").Column("Number of packages").Expect("50");
        }
    }
}