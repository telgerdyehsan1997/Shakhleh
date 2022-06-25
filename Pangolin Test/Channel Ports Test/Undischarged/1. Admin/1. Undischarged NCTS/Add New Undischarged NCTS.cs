using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewUndischargedNCTS : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            //Run<Admin_AddAConsignmentToAnNCTSShipment>();
            var CustomsProTrackingNumber = "CP100000001";
            var MRNNumber = "12GB56789012345678";
            var DateofNotification = "16/02/2022";
            var CustomerReferenceNumber= "123";
            var CustomerName = "TRUCKERS LTD";
            var CustomerAddress = "LOCK VIEW, BASIN ROAD, WORCESTER, WR5 3DA";
            var CustomerAccountNumber = "A1234";
            var VehicleNumber = "t37";
            var TrailerNumber = "t37";
            var Contacts = "Robert Jones - robert.jones@uat.co - QA";
            var ConsignmentValue = "50";
            


            LoginAs<Undischarged_ChannelPortsAdmin>();

            Click("Undischarged NCTS");
            ExpectHeader("Undischarged NCTS");

            ClickLink("New Undischarged NCTS");
            ExpectHeader("Undischarged NCTS Details");

            //No Customs Pro Tracking Number
            Set("Customs Pro Tracking Number").To(CustomsProTrackingNumber);
            Click("Search");
            ExpectText("No value is provided in the Customs Pro Tracking Number field.");
            Click("OK");

            //Fields should be Filled in when add new organization. 
            Set("Customs Pro Tracking Number").To(CustomsProTrackingNumber);
            Click("Search");

            AtField("MRN Number").ExpectValue(MRNNumber);
            AtField("Date of Notification").ExpectValue(DateofNotification);
            AtField("Customer Reference Number").ExpectValue(CustomerReferenceNumber);
            AtField("Customer Name").ExpectValue(CustomerName);
            AtField("Customer Address").ExpectValue(CustomerAddress);
            AtField("Customer Account Number").ExpectValue(CustomerAccountNumber);
            AtField("Vehicle Number").ExpectValue(VehicleNumber);
            AtField("Trailer Number").ExpectValue(TrailerNumber);
            //AtField("Contacts").ExpectValue(Contacts);
            AtField("Consignment Value").ExpectValue(ConsignmentValue);
            AtField("Shortage").Expect("Yes");



            //Expect Cancel/Save buttons
            ExpectButton("Cancel");
            ExpectButton("Save");

            // ---- Cancel button ---
            Click("Cancel");
            ExpectHeader("Undischarged NCTS");

            // ---- Add New ---
            ClickLink("New Undischarged NCTS");
            ExpectHeader("Undischarged NCTS Details");

            //Fill Data: Leave Mandatory fields Blank
            Set("Customs Pro Tracking Number").To("missing LRN");
            Click("Search");
            ExpectText("No consignment with such LRN number, please fill the rest of form manually.");
            Click("OK");
            Set("MRN Number").To("");
            Set("Date of Notification").To("");
            Set("Customer Reference Number").To("");
            Set("Customer Name").To("");
            Set("Customer Address").To("");
            Set("Customer Account Number").To("");
            Set("Vehicle Number").To("");
            Set("Trailer Number").To("");
            Set("Contacts").To("");
            Set("Consignment Value").To("");
            Set("Shortage").To("");
            ClickButton("Save");
            ExpectText(That.Contains, "The Date of Notification field is required.");
            ExpectText(That.Contains, "The Customer Reference Number field is required.");
            ExpectText(That.Contains, "The Customer Name field is required.");
            ExpectText(That.Contains, "The Customer Address field is required.");
            ExpectText(That.Contains, "The Customer Account number field is required.");
            ExpectText(That.Contains, "The Vehicle Number field is required.");
            ExpectText(That.Contains, "The Trailer Number field is required.");
            ExpectText(That.Contains, "The Consignment Value field is required.");

            //Save button
            Set("Customs Pro Tracking Number").To(CustomsProTrackingNumber);
            Set("Date of Notification").To(DateofNotification);
            Set("Shortage").To("Yes");
            Click("Save");

            //Expect row

            ExpectHeader("Undischarged NCTS");

            AtRow(CustomsProTrackingNumber).Column("Customs Pro Tracking Number").ExpectValue(CustomsProTrackingNumber);
            AtRow(CustomsProTrackingNumber).Column("MRN Number").ExpectValue(MRNNumber);
            AtRow(CustomsProTrackingNumber).Column("Date of Notification").ExpectValue(DateofNotification);
            AtRow(CustomsProTrackingNumber).Column("Customer Reference Number").ExpectValue(CustomerReferenceNumber);
            AtRow(CustomsProTrackingNumber).Column("Customer Name").ExpectValue(CustomerName);
            AtRow(CustomsProTrackingNumber).Column("Customer Address").ExpectValue(CustomerAddress);
            AtRow(CustomsProTrackingNumber).Column("Customer Account Number").ExpectValue(CustomerAccountNumber);
            AtRow(CustomsProTrackingNumber).Column("Vehicle Number").ExpectValue(VehicleNumber);
            AtRow(CustomsProTrackingNumber).Column("Trailer Number").ExpectValue(TrailerNumber);
            AtRow(CustomsProTrackingNumber).Column("Contacts").ExpectValue(Contacts);
            AtRow(CustomsProTrackingNumber).Column("Consignment Value").ExpectValue(ConsignmentValue);
            AtRow(CustomsProTrackingNumber).Column("Shortage").Expect("Yes");

        }
    }
}
