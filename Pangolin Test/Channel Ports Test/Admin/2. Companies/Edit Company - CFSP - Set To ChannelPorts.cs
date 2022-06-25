using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCompany_CFSP_SetToChannelPorts : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "CFSP Own Test";
            var editedCompanyName = "CFSP ChannelPorts Test";

            Run<AddCompany_CFSPSetToOwn>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Companies
            ClickLink("Companies");

            ExpectHeader("Companies");

            //Edits the Company and Sets CFSP to ChannelPorts
            AtRow(companyName).Column("Edit").ClickLink("Edit");

            ExpectHeader("Record Details");
            Set("Company name").To(editedCompanyName);
            AtLabel("CFSP").ClickLabel("Channelports");
            AtLabel("Using EIDR?").ClickLabel("Yes");
            ClickField(The.Bottom, "Country");
            System.Threading.Thread.Sleep(1000);
            Expect("GB - UNITED KINGDOM");
            System.Threading.Thread.Sleep(1000);
            Click("GB - UNITED KINGDOM");
            ClickButton("Save");

            //Asserts that the Company has been edited
            ExpectHeader("Companies");
            ExpectRow(editedCompanyName);
        }
    }
}