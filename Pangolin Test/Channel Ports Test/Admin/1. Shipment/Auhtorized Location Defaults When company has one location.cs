using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AuhtorizedLocationDefaultsWhenCompanyHasOneLocation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<JohnSmithCreatesACustomerAccount,AdminChangesTruckersLtdCompanyType,AddRouteSouthamptonAndValencia,AddStop24AsTruckersAuthorisedLocation>();
            LoginAs<JohnSmithCustomer>();

            Click("New Shipment");

            ExpectHeader("Shipment Details");

            ClickLabel("Use authorised location?");


            AtLabel("Authorised location").Expect("Stop 24");

        }
    }
}
