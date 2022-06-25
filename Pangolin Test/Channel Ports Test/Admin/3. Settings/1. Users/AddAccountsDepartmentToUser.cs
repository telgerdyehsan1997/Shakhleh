using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddAccountsDepartmentToUser : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            ExpectHeader("Shipments");
            Click("Companies");

            AtRow("Imports Ltd").Click("Imports Ltd");
            ExpectHeader("Imports Ltd");
            ClickLink("Company Users");

            //add user witth Accounts ticked
            Click("New Company User");
            Set("First name").To("Rafal");
            Set("Last name").To("QA");
            Set("Email address").To("raf@uat.co");
            AtLabel("Accounts department").ClickLabel("Yes");
            AtLabel("Customer Admin").ClickLabel("No");
            Click("Save");

            ExpectColumn("Accounts department");
            AtRow("Rafal").Column("Accounts department").ExpectTick();

            //add user without acc ticked
            Click("New Company User");
            Set("First name").To("Rafal");
            Set("Last name").To("Second");
            Set("Email address").To("second@uat.co");
            AtLabel("Accounts department").ClickLabel("No");
            AtLabel("Customer Admin").ClickLabel("No");
            Click("Save");

            //---checking if Rafal QA is visible--------------
            Click("Shipments");
            Click("New Shipment");

            ClickHeader("Shipment Details");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports");
            ClearField("Primary Contact");
            ClickHeader("Shipment Details");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "RAFAL SECOND");

            //----NCTS Shipment------------------
            Click("NCTS Shipments Out of UK");
            Click("New NCTS Shipment Out of UK");
            ExpectHeader("Shipment details");

            ClickField("Company name");
            ClickField("Company name");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "Imports");
            System.Threading.Thread.Sleep(1000);
            Click(What.Contains, "Imports");
            ClearField("Primary Contact");
            ClickHeader("Shipment Details");
            ClickField("Primary contact");
            System.Threading.Thread.Sleep(1000);
            Expect(What.Contains, "RAFAL SECOND");

            Click("Cancel");

            ExpectHeader("NCTS Shipments Out of UK");
        }
    }
}