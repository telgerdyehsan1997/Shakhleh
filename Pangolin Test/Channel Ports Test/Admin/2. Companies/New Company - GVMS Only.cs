using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChannelPorts.Pangolin.UI_Constants;

namespace Channel_Ports_Test
{
    [TestClass]
    public class NewCompany_GVMSOnly : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "GVMS Only";
            var companyCountry = "GB - UNITED KINGDOM";
            var gvmsType = GVMSConstants.Always;
            var outBound = SecurityOutboundConstants.None;

            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            this.NavigateToCompanies();

            ClickLink("New Company");
            ExpectHeader("Record Details");

            //Creates the new Company
            AtLabel("Type").ClickLabel("Customer");

            //Sets the Company to 'GVMS' only
            AtLabel("GVMS").ClickLabel(gvmsType);
            AtLabel("Safety and security outbound").ClickLabel(outBound);
            Set("Customer account number").To("A9987");
            Set("Company name").To(companyName);
            ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            Expect(companyCountry);
            System.Threading.Thread.Sleep(1000);
            Click(companyCountry);
            System.Threading.Thread.Sleep(1000);
            Set(That.Contains, "Postcode").To("SM1 8AY");
            Set("Address line 1").To("Sutton Road");
            Set("Town/City").To("Sutton");
            Set("EORI number").To("GB243512587000");
            AtLabel("CFSP").ClickLabel("None");
            AtLabel("Guarantor Type").ClickLabel("None");

            //Saves the Company
            ClickButton("Save");
            ExpectHeader("Companies");
            ExpectRow(companyName);
        }
    }
}