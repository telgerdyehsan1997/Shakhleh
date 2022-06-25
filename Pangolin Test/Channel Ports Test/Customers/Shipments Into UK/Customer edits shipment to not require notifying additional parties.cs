using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CustomerEditsShipmentToNotRequireNotifyingAdditionalParties : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<JennySmithAddsShipmentForWWL_IntoUK>();
            LoginAs<JennySmithCustomer>();

            AssumeDate("01/01/2020");
            Goto("/");

            Click(The.Top, "Shipments Into Uk");
            WaitToSeeHeader("Shipments Into UK");

            Set("Date created").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            ClickButton("Search");

            ExpectRow("R0220000001");
            AtRow("R0220000001").Click("Edit");

            WaitToSeeHeader("Shipment Details");
            Click(What.Contains, "Save and Add");

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");

            Set("Date created").To("01/01/1999");
            Set("Expected date of arrival/departure").To("01/01/1999");
            Set(The.Top, "to").To("25/12/2025");
            Set(The.Bottom, "to").To("25/12/2025");
            ClickButton("Search");


            ExpectRow("R0220000001");

            Click("R0220000001");

            WaitToSee(What.Contains, "Shipment Details");

            AtLabel("Notify additional parties").Expect("Not required");

            ExpectNoLabel("Group");
            ExpectNoLabel("Contact name");
        }
    }
}