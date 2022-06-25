using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ChangeConsignmentToWithImporter : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddConsignmentToTruckersLtd>();
            //navigate
            LoginAs<ChannelPortsAdmin>();

            //case passes without any issues, just adding this so I would be able to send update to sourcetree - Raf

            WaitToSeeHeader("Shipments");
            Goto("/");
            Set("Date created").To("28/06/2021");
            Set("Expected date of arrival/departure").To("28/06/2021");
            Set("Date created").To("28/06/1999");
            Set("Expected date of arrival/departure").To("28/06/1999");
            Set(The.Top, "to").To("25/12/2022");
            Set(The.Bottom, "to").To("25/12/2022");
            ClickButton("Search");
            WaitToSeeHeader("Shipments");
            AtRow("R0721000001").Column("Tracking number").ClickLink();
            AtRow("R072100000101").Column("Progress").Click("Draft");
            Set("Progress").To("WithImporter");
            ClickButton("Save");
            ClickLink("Shipments");

            /*AtRow(That.Contains, "R0721000001").Column("With Importer").ClickButton();
            AtRow(That.Contains, "R0721000001").Column("With Importer").ExpectNoButton(); */
            AtRow(That.Contains, "R0721000001").Column("Progress").Expect("With Importer");
        }
    }
}
