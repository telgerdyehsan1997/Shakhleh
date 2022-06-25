using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_AddCustomerEmailRecipient_Aviation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customerAccountNumber = "Z9999";
            var customerName = "Channel Ports";
            var firstName = "aviation";
            var lastName = "test";
            var name = $"{firstName} {lastName}";
            var jobTitle = "QA";
            var email = "aviation@uat.co";
            var telephone = "02035070033";

            Run<Undischarged_AddRecognisedCustomer>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            ClickLink("Customers");
            ExpectHeader("Customers");
            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Email Recipients").Click("0");

            WaitToSeeHeader($"[#{customerName}#] - Email Recipients");

            ClickLink("New Email Recipient");
            ExpectHeader("Email Recipient Details");
            Set("First Name").To(firstName);
            Set("Last Name").To(lastName);
            Set("Job Title").To(jobTitle);
            Set("Email").To(email);
            Set("Telephone").To(telephone);
            ClickButton("Save");

            ExpectRow(email);
            AtRow(email).Column("Name").Expect(name);
            AtRow(email).Column("Job Title").Expect(jobTitle);
            AtRow(email).Column("Email").Expect(email);
            AtRow(email).Column("Telephone").Expect(telephone);
        }
    }
}