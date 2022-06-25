using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewCountry_France : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<CreateNewCPC_12345, CreateNewCPC_54321>();
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            // Navigation
            Click("Settings");
            WaitForNewPage();
            Expect("Countries");
            Click("Countries");
            WaitToSeeHeader("Countries");
            Click("New Country");
            WaitToSeeHeader("Country Details");

            // ----------------------------------------------

            // Set France details
            Set("Country").To("France");
            Set("Country code").To("FR");
            AtLabel("Eu27").ClickLabel("Yes");
            AtLabel("MFN").ClickLabel("No");
            AtLabel("Preference available").ClickLabel("Yes");
            AtLabel("MFN").ClickLabel("No");

            Set("Import CPC with preference declaration type").To("AA");
            Set("Import CPC with preference preference code").To("111");
            Set("Import CPC with preference Rate Code").To("UT");
            Set("Export CPC without preference").To("CP54321");
            Click(What.Contains, "CP54321");


            Set("Import CPC without preference").To("CP12345");
            Click(What.Contains, "CP12345");

            Set("Import CPC without preference declaration type").To("IM");
            Set("Import CPC without preference preference code").To("123");
            Set("Import CPC without preference Rate Code").To("F");
            Set("Export CPC with preference").To("CP54321");
            Click(What.Contains, "CP54321");

            Set("Export CPC with preference declaration type").To("EX");

            Set("Invoice declaration document type").To("AB12");
            Set("Invoice declaration document type document status").To("C3");
            Set("Preference certificate number document type").To("DE56");
            Set("Preference certificate number document type document status").To("F7");
            Set("Country dialling code").To("+33");

            Set("Import CPC with preference").To("1000001");
            Set("Export CPC without preference declaration type").To("Ab");

            Click("Save");

            ExpectHeader("Countries");
            ExpectRow("France");

            // ----------------------------------------------

            /*/ Expect
            WaitToSeeHeader("Countries");
            ExpectRow("France");
            AtRow("France").Column("Country code").Expect("FR");
            AtRow("France").Column("EU27").ExpectTick();
            //AtRow("France").Column("MFN code").Expect("");
            AtRow("France").Column("Preference available").ExpectNoTick();
            //AtRow("France").Column("Import CPC with preference").Expect("");
            //AtRow("France").Column("Import CPC with preference declaration type").Expect("");
            //AtRow("France").Column("Import CPC with preference preference code").Expect("");
            AtRow("France").Column("Import CPC without preference").Expect("CP12345");
            AtRow("France").Column("Import CPC without preference declaration type").Expect("IMP");
            AtRow("France").Column("Import CPC without preference preference code").Expect("123");
            //AtRow("France").Column("Export CPC with preference").Expect("");
            //AtRow("France").Column("Export CPC with preference declaration type").Expect("");
            AtRow("France").Column("Export CPC without preference").Expect("CP54321");
            AtRow("France").Column("Export CPC without preference declaration type").Expect("EXP");
            AtRow("France").Column("Invoice declaration document type").Expect("AB12");
            AtRow("France").Column("Invoice declaration document type document status").Expect("C3");
            AtRow("France").Column("Preference certificate number document type").Expect("DE56");
            AtRow("France").Column("Preference certificate number document type document status").Expect("F7");
            AtRow("France").Column("Country dialling code").Expect("+33");
            */

        }
    }
}
