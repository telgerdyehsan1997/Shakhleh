using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminEditsCompanyWorldwideLogisticsLtdtoNCTS : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<CreateNewCountry_France, AdminAddsCompanyWorldwideLogisticsLtd>();
            //login as admin
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            //edit company WWL
            AtRow("Worldwide Logistics Ltd").Column("Edit").Click("Edit");
            WaitToSeeHeader(That.Contains, "Record Details");
            ClickLabel("Out of UK");
            AtLabel("NCTS").ClickLabel("Sometimes");
            //AtLabel("GVMS").ClickLabel("Sometimes");
            Click("Save");

            // ----------------------------------------------

            //assert details in list
            WaitToSeeHeader("Companies");
            ExpectRow("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Column("Company name").Expect("Worldwide Logistics Ltd");
            AtRow("Worldwide Logistics Ltd").Column("Customer account number").Expect("A1235");
            AtRow("Worldwide Logistics Ltd").Column("Address").Expect("Lock Keepers Cottage, Basin Road, Worcester, WR5 3DA");
            AtRow("Worldwide Logistics Ltd").Column("Country").Expect("FR");
            AtRow("Worldwide Logistics Ltd").Column("Type").Expect("Customer");
            AtRow("Worldwide Logistics Ltd").Column("Payment type").Expect("A - CODE A");
            AtRow("Worldwide Logistics Ltd").Column("Deferment number").Expect("1234567");
        }
    }
}
