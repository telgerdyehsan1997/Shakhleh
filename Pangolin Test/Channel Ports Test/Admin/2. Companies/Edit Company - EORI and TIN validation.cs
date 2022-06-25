using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCompany_EORIAndTINValidation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AdminAddsCompanyTruckersLtd>();

            // ----------------------------------------------

            // Navigation
            LoginAs<ChannelPortsAdmin>();

            Click("Companies");
            WaitToSeeHeader("Companies");

            // ----------------------------------------------

            AtRow("Truckers Ltd").Click("Edit");
            WaitToSeeHeader(That.Contains, "Record Details");

            // check required fields - EORI non-mandatory for non-UK/GB
            Set("EORI number").To("");
            Set("TIN").To("");

            Click("Save");

            Expect("TIN must have value if Transit Guarantee and Guarantee type have a value.");
            ClickButton("OK");
            Set("PIN").To("");
            AtLabel("Guarantor Type").ClickLabel("None");
            ClickButton("Save");

            AtRow("Truckers Ltd").Click("Edit");
            WaitToSeeHeader(That.Contains, "Record Details");

            // check required fields - EORI mandatory for UK/GB
            Set("Country").To("");
            ClickField("Country");
            Type("United Kingdom");
            Click(The.Bottom, "GB - United Kingdom");

            Click("Save");

            Expect("The EORI number field is required.");
            ExpectNo("The TIN field is required.");

            // check valid format
            Set("EORI number").To("GB34567891012");
            Set("TIN").To("GB34567891012");

            Click("Save");

            Expect(What.Contains, "The EORI number is invalid.");
            Expect(What.Contains, "The TIN is invalid.");

            Click("OK");

            Set("EORI number").To("1234567891012");
            Set("TIN").To("T1234567891012");

            Click("Save");

            Expect(What.Contains, "The EORI number is invalid.");
            Expect(What.Contains, "The TIN is invalid.");

            Click("OK");

            // check valid length of numbers (must be 14 characters long)
            Set("EORI number").To("TE34567890123");
            Set("TIN").To("TE34567890123");

            Click("Save");

            Expect(What.Contains, "The EORI number is invalid.");
            Expect(What.Contains, "The TIN is invalid.");

            Click("OK");

            Set("EORI number").To("TE3456789012345");
            Set("TIN").To("TE3456789012345");

            Click("Save");

            Expect(What.Contains, "The EORI number is invalid.");
            Expect(What.Contains, "The TIN is invalid.");

            Click("OK");

            // No validation needed if not starting with "GB"
            Set("EORI number").To("TE345678901234");
            Set("TIN").To("TE345678901234");

            Click("Save");

            ExpectNo(What.Contains, "The EORI number is invalid.");
            ExpectNo(What.Contains, "The TIN is invalid.");

            ExpectHeader("Companies");
        }
    }
}