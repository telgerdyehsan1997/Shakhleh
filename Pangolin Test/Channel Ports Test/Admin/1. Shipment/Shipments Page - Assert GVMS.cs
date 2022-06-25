using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ShipmentsPage_AssertGVMS : UITest
    {
        [Ignore]
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION - SOTENBORI - OSA OB1 - 2134567";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2>();

            LoginAs<ChannelPortsAdmin>();

            //Navigates to Shipment
            ClickLink("New Shipment");
            ExpectHeader("Shipment Details");

            //Sets Company with GVMS selected
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, companyName);
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, companyName);

            //Asserts Labels when GVMS Company is selected
            ExpectLabel("GVMS");
            ExpectLabel("Are there MRNs");
            ExpectLabel("Unaccompanied trailer");
            ExpectLabel("Container Number");

            //Sets MRNs to yes to Assert MRN fields
            AtLabel("Are there MRNs?").ClickLabel("Yes");
            ExpectLabel("Are MRNS available now?");
            AtLabel("Are MRNS available now?").ClickLabel("Yes");
            ExpectField("MRN");

            //Sets MRNs to no to Assert no MRN fields
            AtLabel("Are MRNS available now?").ClickLabel("No");
            ExpectNoLabel("MRN");
            AtLabel("Are there MRNs?").ClickLabel("No");
            ExpectNoLabel("Are MRNS available now?");
        }
    }
}