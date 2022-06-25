using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditAdminUndischargedNCTS_Amazon : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddUndischargedNCTS_FillingForm_Amazon>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            var CustomsProTrackingNumber = "CP100000001";
            var MRNNumber = "12GB56789012345678";
            var DateofNotification = "16/02/2022";
            var CustomerReferenceNumber = "123";
            var CustomerName = "TRUCKERS LTD";
            var CustomerAddress = "LOCK VIEW, BASIN ROAD, WORCESTER, WR5 3DA";
            var CustomerAccountNumber = "A1234";
            var VehicleNumber = "t37";
            var TrailerNumber = "t37";
            //var Contacts = "Robert Jones - robert.jones@uat.co - QA";
            var ConsignmentValue = "50";
            var Shortage = "Yes";

            //Navigation
            AtRow(CustomsProTrackingNumber).Column("Edit").ClickLink();

            //Filling Form
            Set("Customs Pro Tracking Number").To(CustomsProTrackingNumber);
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

            AtRow(CustomsProTrackingNumber).Column("Customs Pro Tracking Number").ExpectValue(CustomsProTrackingNumber);
            AtRow(CustomsProTrackingNumber).Column("MRN Number").ExpectValue(MRNNumber);
            AtRow(CustomsProTrackingNumber).Column("Date of Notification").ExpectValue(DateofNotification);
            AtRow(CustomsProTrackingNumber).Column("Customer Reference Number").ExpectValue(CustomerReferenceNumber);
            AtRow(CustomsProTrackingNumber).Column("Customer Name").ExpectValue(CustomerName);
            AtRow(CustomsProTrackingNumber).Column("Customer Address").ExpectValue(CustomerAddress);
            AtRow(CustomsProTrackingNumber).Column("Customer Account Number").ExpectValue(CustomerAccountNumber);
            AtRow(CustomsProTrackingNumber).Column("Vehicle Number").ExpectValue(VehicleNumber);
            AtRow(CustomsProTrackingNumber).Column("Trailer Number").ExpectValue(TrailerNumber);
            //AtRow(CustomsProTrackingNumber).Column("Contacts").ExpectValue(Contacts);
            AtRow(CustomsProTrackingNumber).Column("Consignment Value").ExpectValue(ConsignmentValue);
            AtRow(CustomsProTrackingNumber).Column("Shortage").Expect("Yes");
        }
    }
}