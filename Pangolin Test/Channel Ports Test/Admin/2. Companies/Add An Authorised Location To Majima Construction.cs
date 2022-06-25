using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAnAuthorisedLocationToMajimaConstruction : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var companyName = "MAJIMA CONSTRUCTION";
            var authorisedLocation = "WAREHOUSE 1";

            Run<AddCompanyMajimaConstruction_DefNumberStartsWith2, AddGuaranteeLength10DaysForWareHouse1>();
            LoginAs<ChannelPortsAdmin>();

            //Navigate to Companies
            Click("Companies");
            WaitToSeeHeader("Companies");

            AtRow(companyName).Column("Edit").Click("Edit");
            Set("Authorised locations").To(authorisedLocation);
            Click("Save");

            ExpectHeader("Companies");
        }
    }
}