using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditCompany_CFSPOwn_EnableSFD : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "CFSP OWN TEST";

            Run<AddCompany_CFSPSetToOwn>();
            LoginAs<ChannelPortsAdmin>();

            //Naigates to Companies
            ClickLink("Companies");
            ExpectHeader("Companies");

            //Edits the Company and sets SFD to 'Yes'
            AtRow(companyName).Column("Edit").ClickLink();

            ExpectHeader("Record Details");
            AtLabel("SFDOnly").ClickLabel("Yes");
            AtLabel("Using EIDR?").ClickLabel("No");
            ClickButton("Save");

            ExpectHeader("Companies");
            ExpectRow(companyName);
        }
    }
}