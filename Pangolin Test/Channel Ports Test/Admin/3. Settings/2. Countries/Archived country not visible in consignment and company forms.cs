using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ArchivedCountryNotVisibleInConsignmentAndCompanyForms : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "113085")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            /*
            //check country in consignment form
            WaitToSeeHeader(That.Contains, "Pre-Advice");
            AtRow(The.Top).Column("Edit").Click("Edit");
            WaitToSeeHeader("Advice details");
            Click(What.Contains, "Nothing selected");
            Type("Jack");
            Press(Keys.Enter);
            Click("Save and Add/Amend Consignments");
            WaitToSeeHeader(That.Contains, "Consignments");
            Click("New Consignment");
            WaitToSeeHeader("Consignment Details");
            Type("Ltd");
            Click(What.Contains, "Imports");
            ClickField("Country of destination");
            Type("Kingdom");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "United");

            //check country in consignment > add company modal form
            Near("UK trader").ClickButton("AddCompany");
            ClickField("Country");
            System.Threading.Thread.Sleep(1000);
            Type("Kingdom");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "United");
            Click(The.Top, "Cancel");
            */

            //check country in company form
            ClickLink("Companies");
            WaitToSeeHeader(That.Contains, "Companies");
            Click("New Company");
            ClickField("Country");
            Type("Kingdom");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "United");

            //archive country
            Click(The.Top, "Settings");
            Click("Countries");
            AtRow(That.Contains, "United Kingdom").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            Click(The.Left, "Archive");
            WaitToSeeHeader(That.Contains, "Countries");
            ExpectNoRow(That.Contains, "United Kingdom");
            NearLabel("Active").ClickLabel("Archived");
            Click("Search");
            ExpectRow(That.Contains, "United Kingdom");

            //check country not in company form
            Click(The.Top, "Companies");
            WaitToSeeHeader(That.Contains, "Companies");
            Click("New Company");
            ClickField("Country");
            Type("Kingdom");
            System.Threading.Thread.Sleep(1000);
            ExpectNo(What.Contains, "United");
        }
    }
}