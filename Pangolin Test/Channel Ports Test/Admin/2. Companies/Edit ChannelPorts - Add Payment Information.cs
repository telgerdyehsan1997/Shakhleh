using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditChannelPorts_AddPaymentInformation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "CHANNEL PORTS";

            Run<PaymentTypeA>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            ClickLink("Companies");

            ExpectHeader("Companies");

            //Adds payment details for ChannelPorts
            AtRow(companyName).Column("Edit").ClickLink("Edit");

            AtLabel("Safety and security inbound").ClickLabel("No Safety And Security");
            AtLabel("Safety and security outbound").ClickLabel("No Safety And Security");
            Set("AEO number").To("GBAEOC0024316");
            AtLabel("CFSP").ClickLabel("None");
            Set("Payment type").To("A - CODE A");
            Set("Deferment number").To("8100765");
            AtLabel("VAT by DAN").ClickLabel("No");
            Set("Transit Guarantee").To("20GB0000010001G66");
            Set("Guarantee type").To("0");
            Set("TIN").To("GB683470514000");
            Set("PIN").To("AS01");
            Set("Authorised locations").To("STOP 24");
            ClickButton("Save");

            //Asserts that Edits have been saved
            AtRow(companyName).Column("Deferment number").Expect("8100765");
        }
    }
}