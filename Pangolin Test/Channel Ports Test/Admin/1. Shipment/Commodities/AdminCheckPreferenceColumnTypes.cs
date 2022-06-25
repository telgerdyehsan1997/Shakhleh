using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AdminCheckPreferenceColumnTypes : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddThreeCommoditiesWithPreference>();

            LoginAs<ChannelPortsAdmin>();

            ExpectHeader("Shipments");

            AtRow("R0221000001").Column("Consignments").ClickLink();
            AtRow("R022100000101").Column("Commodities").ClickLink();

            ExpectRow(That.Contains, "Invoice declaration");
            ExpectRow(That.Contains, "Preference certificate number");
            ExpectRow(That.Contains, "Statement of origin_ importers knowledge");

            //---CheckingInvoiceCurrencyDataSource goverment link
            AtRow(That.Contains, "Statement of origin_ importers knowledge").Column("Edit").Click("Edit");
            ClearField("Country of origin");
            ClickField("Country of origin");
            Type("FR");
            System.Threading.Thread.Sleep(1000);
            Click(The.Bottom, What.Contains, "FRANCE");
            BelowText("Statement of origin_ importers knowledge").ExpectLink(That.Contains, "Statement of Origin-Importers Knowledge");
            System.Threading.Thread.Sleep(3000);
            Click("Save");

            ExpectRow(That.Contains, "Invoice declaration");
            ExpectRow(That.Contains, "Preference certificate number");

        }
    }
}