using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageCountries : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<ArchiveCountryUK, CreateNewCountry_France>();
            // ----------------------------------------------

            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Countries");
            Click("Countries");
            WaitToSeeHeader("Countries");
            ExpectNoRow("United Kingdom");
            ExpectRow("France");

            // ----------------------------------------------

            // Edit - Cancel
            AtRow("France").Column("Country").ClickLink();
            WaitToSeeHeader("Country Details");
            Set("Country").To("Sweden");
            Set("Country code").To("SW");
            AtLabel("Preference available").ClickLabel("No");

            ExpectNoField("Import CPC with preference");
            ExpectNoField("Import CPC with preference declaration type");
            ExpectNoField("Import CPC with preference preference code");
            ExpectNoField("Export CPC with preference");
            ExpectNoField("Export CPC with preference declaration type");
            AtLabel("Preference available").ClickLabel("Yes");
            ExpectField("Import CPC with preference");
            ExpectField("Import CPC with preference declaration type");
            ExpectField("Import CPC with preference preference code");
            ExpectField("Export CPC with preference");
            ExpectField("Export CPC with preference declaration type");
            AtLabel("Preference available").ClickLabel("No");

            Set("Import CPC without preference declaration type").To("DE");
            Set("Export CPC without preference declaration type").To("FE");

            Click("Cancel");
            ExpectNoRow("Sweden");
            ExpectRow("France");

            // ----------------------------------------------

            // Edit - Save
            AtRow("France").Column("Country").ClickLink();
            WaitToSeeHeader("Country Details");
            Set("Country").To("Sweden");
            Set("Country code").To("SW");
            Set("Import CPC without preference declaration type").To("DE");
            Set("Export CPC without preference declaration type").To("FE");
            Click("Save");
            ExpectNoRow("France");
            ExpectRow("Sweden");
            AtRow("Sweden").Column("Import CPC without preference declaration type").Expect("DE");
            AtRow("Sweden").Column("Export CPC without preference declaration type").Expect("FE");

            // ----------------------------------------------

            // Status
            ClickLabel("Active");
            Click("Search");
            ExpectRow("Sweden");
            ExpectNoRow("United Kingdom");

            ClickLabel("Archived");
            Click("Search");
            ExpectNo("Italy");
            ExpectRow("United Kingdom");

            ClickLabel("All");
            Click("Search");
            Expect("Sweden");
            ExpectRow("United Kingdom");

            // Validation messages
            Click("New Country");

            WaitToSeeHeader("Country Details");

            // Validate empty fields - preference unchecked
            Click("Save");
            Expect(What.Contains, "The Name field is required");
            Expect(What.Contains, "The Code field is required");
            Expect(What.Contains, "The Import CPC without preference field is required");
            Expect(What.Contains, "The Import CPC without preference declaration type field is required");
            Expect(What.Contains, "The Import CPC without preference preference code field is required");
            Expect(What.Contains, "The Export CPC without preference field is required");
            Expect(What.Contains, "The Export CPC without preference declaration type field is required");
            Expect(What.Contains, "The Invoice declaration document type field is required");
            Expect(What.Contains, "The Invoice declaration document type document status field is required");
            Expect(What.Contains, "The Preference certificate number document type field is required");
            Expect(What.Contains, "The Preference certificate number document type document status field is required");

            // Validate empty fields - preference checked
            AtLabel("Preference available").ClickLabel("Yes");
            Click("Save");
            Expect(What.Contains, "The Import CPC with preference field is required");
            Expect(What.Contains, "The Import CPC with preference declaration type field is required");
            Expect(What.Contains, "The Import CPC with preference preference code field is required");
            Expect(What.Contains, "The Export CPC with preference field is required");
            Expect(What.Contains, "The Export CPC with preference declaration type field is required");

            // Validate empty fields - preference checked
            AtLabel("MFN").ClickLabel("Yes");
            Click("Save");
            Expect(What.Contains, "The MFN code 1 field is required");
            ExpectNo(What.Contains, "The MFN code 2 field is required");
            ExpectNo(What.Contains, "The MFN code 3 field is required");
            ExpectNo(What.Contains, "The MFN code 4 field is required");
            ExpectNo(What.Contains, "The MFN code 5 field is required");

            // Validation popup messages - length and uniqueness
            Set("Country").To("United Kingdom");
            // Set("Country code").To("ABC");
            //Expect(What.Contains, "Code should not exceed 2 characters");
            Set("Country code").To("GB");
            Set("MFN code 1").To("1234");
            Set("MFN code 2").To("1234");
            Set("MFN code 3").To("1234");
            Set("MFN code 4").To("1234");
            Set("MFN code 5").To("1234");
            Set("Import CPC with preference declaration type").To("1234");
            Set("Import CPC with preference preference code").To("ABC");
            Set("Import CPC without preference declaration type").To("1234");
            Set("Import CPC without preference preference code").To("ABC");
            Set("Export CPC with preference declaration type").To("1234");
            Set("Export CPC without preference declaration type").To("1234");
            Set("Invoice declaration document type").To("12345");
            Set("Invoice declaration document type document status").To("123");
            Set("Preference certificate number document type").To("12345");
            Set("Preference certificate number document type document status").To("123");

            Click("Cancel");

            AtRow("Sweden").Column("Country").ClickLink();
            WaitToSeeHeader("Country Details");
            Set("Country").To("United Kingdom");
            Set("Country code").To("GB");

            Click("Save");

            Expect(What.Contains, "There is an existing Country with the same Code and Name in the database already.");
        }
    }
}
