using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class AddUndischargedNCTS_FillingForm_Amazon : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            //Run<AddDraftConsignmentToNCTSShipment>();
            Run< Undischarged_AddNewCustomer>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            var CustomsProTrackingNumber = "CP100009901";
            var MRNNumber = "12GB00000000009901";
            var DateofNotification = "26/02/2022";
            var CustomerReferenceNumber = "321";
            var CustomerName = "Amazon";
            var CustomerAddress = "LOCK VIEW, BASIN ROAD, WORCESTER, WR5 3DA";
            //var CustomerAccountNumber = "B1234";
            var CustomerAccountNumber = "Z1111";
            var VehicleNumber = "t48";
            var TrailerNumber = "t48";
            //var Contacts = "Robert Jones - robert.jones@uat.co - QA";
            var ConsignmentValue = "10.00";
            var Shortage = "No";

            ClickLink("Undischarged NCTS");
            
            //Navigation
            ClickLink("New Undischarged NCTS");
            ExpectHeader("Undischarged NCTS Details");

            //Filling Form
            Set("Customs Pro Tracking Number").To(CustomsProTrackingNumber);
            Click("Search");
            //ClickButton(The.Top, "OK");
            AtXPath("/html/body/section[1]/div/article/nav/button").ClickButton("OK");

            
            Set("MRN Number").To(MRNNumber);
            Set("Date of Notification").To(DateofNotification);
            Set("Customer Reference Number").To(CustomerReferenceNumber);
            //Set("Customer Name").To(CustomerName);
            //Set("Customer Address").To(CustomerAddress);
            Set("Customer Account Number").To(CustomerAccountNumber);
            Set("Vehicle Number").To(VehicleNumber);
            Set("Trailer Number").To(TrailerNumber);
            //Set("Contacts").To(Contacts);
            Set("Consignment Value").To(ConsignmentValue);
            Set("Shortage").To(Shortage);
            
            ClickButton("Save");

            ExpectHeader("Undischarged NCTS");
            ExpectRow(That.Contains, CustomsProTrackingNumber);
            AtRow(CustomsProTrackingNumber).Column("Customs Pro Number").Expect(CustomsProTrackingNumber);
            AtRow(CustomsProTrackingNumber).Column("MRN Number").Expect(MRNNumber);
            //AtRow(CustomsProTrackingNumber).Column("Customer Name").Expect(CustomerName);
            AtRow(CustomsProTrackingNumber).Column("Customer Reference Number").Expect(CustomerReferenceNumber);
            AtRow(CustomsProTrackingNumber).Column("Customer Account Number").Expect(CustomerAccountNumber);
            AtRow(CustomsProTrackingNumber).Column("Vehicle Number").Expect(VehicleNumber);
            AtRow(CustomsProTrackingNumber).Column("Trailer Number").Expect(TrailerNumber);
            AtRow(CustomsProTrackingNumber).Column("Consignment Value").Expect(ConsignmentValue);
            AtRow(CustomsProTrackingNumber).Column("Shortage").Expect("");
            AtRow(CustomsProTrackingNumber).Column("Date of Notification").Expect(DateofNotification);
        }
    }
}