using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class AddUndischargedNCTS_FillingForm_Microsoft : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<Undischarged_ChannelPortsAdmin>();

            var CustomsProTrackingNumber = "CP100009902";
            var MRNNumber = "12GB00000000009902";
            var DateofNotification = "16/02/2022";
            var CustomerReferenceNumber = "123";
            var CustomerName = "Microsoft";
            var CustomerAddress = "LOCK VIEW, BASIN ROAD, WORCESTER, WR5 3DA";
            var CustomerAccountNumber = "A1234";
            var VehicleNumber = "t37";
            var TrailerNumber = "t37";
            //var Contacts = "Robert Jones - robert.jones@uat.co - QA";
            var ConsignmentValue = "50";
            var Shortage = "Yes";

            //Navigation
            ClickLink("New Undischarged NCTS");
            ExpectHeader("Undischarged NCTS Details");

            //Filling Form
            Set("Customs Pro Tracking Number").To(CustomsProTrackingNumber);
            Click("Search");
            Set("MRN Number").To(MRNNumber);
            Set("Date of Notification").To(DateofNotification);
            Set("Customer Reference Number").To(CustomerReferenceNumber);
            Set("Customer Name").To(CustomerName);
            Set("Customer Address").To(CustomerAddress);
            Set("Customer Account Number").To(CustomerAccountNumber);
            Set("Vehicle Number").To(VehicleNumber);
            Set("Trailer Number").To(TrailerNumber);
            //Set("Contacts").To(Contacts);
            Set("Consignment Value").To(ConsignmentValue);
            Set("Shortage").To(Shortage);
            ClickButton("Save");

            ExpectHeader("Undischarged NCTS");
            ExpectRow(That.Contains, CustomsProTrackingNumber);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Customs Pro Number").ExpectValue(CustomsProTrackingNumber);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("MRN Number").ExpectValue(MRNNumber);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Customer Name").ExpectValue(CustomerName);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Customer Reference Number").ExpectValue(CustomerReferenceNumber);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Customer Account Number").ExpectValue(CustomerAccountNumber);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Vehicle Number").ExpectValue(VehicleNumber);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Trailer Number").ExpectValue(TrailerNumber);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Consignment Value").ExpectValue(ConsignmentValue);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Shortage").Expect(Shortage);
            AtRow(That.Contains, CustomsProTrackingNumber).Column("Date of Notification").ExpectValue(DateofNotification);
        }
    }
}