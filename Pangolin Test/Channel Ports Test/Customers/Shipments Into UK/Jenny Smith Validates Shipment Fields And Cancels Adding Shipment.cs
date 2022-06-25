using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class JennySmithValidatesShipmentFieldsAndCancelsAddingShipment : UITest
    {
        [TestProperty("Sprint", "2")]
        [TestProperty("AMP", "112147")]

        [PangolinTestMethod]
        public override void RunTest()
        {

            Run<AdminAddsCompanyWorldwideLogisticsLtd, AddCompanyUserForWWLJenny, AddCompanyUserForWWLRichardSmith>();
            LoginAs<JennySmithCustomer>();

            Click(The.Top, "Shipments Into UK");
            WaitToSeeHeader("Shipments Into UK");
            Click("New Shipment");

            WaitToSeeHeader("Shipment Details");

            // Visible to Customer users
            Expect("Primary contact");
            ExpectField("Customer Reference");
            ExpectField("Type");
            ExpectField("Vehicle number");
            ExpectField("Trailer number");
            ExpectLabel("Upload attachments");
            ExpectButton("Add Another Attachment");
            ExpectButton("Save and Add/Amend Consignments");

            // Should only be available for ChannelPorts users
            ExpectNoField("Company name");
            ExpectNoField("Company type");
            ExpectNoField("Contact type");


            Click("Save and Add/Amend Consignments");

            ExpectText("The Primary contact field is required.");

            ClickLabel(That.Contains, "Primary contact");
            Type("Richard Smith");
            System.Threading.Thread.Sleep(3000);
            Press(Keys.Enter);
            Set("Customer Reference").To("12345");
            ClickLabel("Out of UK");

            Click("Save and Add/Amend Consignments");

            Expect("The Expected date field is required.");
            //Expect("The Port of arrival field is required.");

            Click("Shipments Out of UK");
            WaitToSeeHeader("Shipments Out of UK");
            ClickLabel(The.Top, "All");
            Click("Search");
            ExpectNoRow("12345");
        }
    }
}
