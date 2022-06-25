using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_CancelEditCustomerEmailRecipient_Aviation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Undischarged_AddCustomerEmailRecipient_Aviation>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            AtLabel("Email Recipients").ClickRadiobox(The.Bottom);
            Click("Save");

            var customerAccountNumber = "Z9999";
            var customerName = "Channel Ports";
            var firstName = "test1";
            var lastName = "test2";
            var name = $"{firstName} {lastName}";
            var jobTitle = "QA";
            var editedJobTitle = "DEV";
            var email = "test1@uat.co";
            var telephone = "02035070033";

            Run<Undischarged_AddRecognisedCustomer>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            ClickLink("Customers");
            ExpectHeader("Customers");
            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Email Recipients").Click("1");

            WaitToSeeHeader($"[#{customerName}#] - Email Recipients");

            AtRow(email).Column("Edit").Click("Edit");
            ExpectHeader("Email Recipient Details");
            //Set("First Name").To(firstName);
            //Set("Last Name").To(lastName);
            Set("Job Title").To(editedJobTitle);
            //Set("Email").To(email);
            //Set("Telephone").To(telephone);
            ClickButton("Cancel");

            ExpectRow(email);
            AtRow(email).Column("Name").Expect(name);
            AtRow(email).Column("Job Title").Expect(jobTitle);
            AtRow(email).Column("Email").Expect(email);
            AtRow(email).Column("Telephone").Expect(telephone);
        }
    }
}