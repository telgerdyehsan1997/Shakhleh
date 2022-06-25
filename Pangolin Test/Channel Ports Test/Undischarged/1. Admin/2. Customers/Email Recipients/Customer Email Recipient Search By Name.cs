using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_CustomerEmailRecipient_SearchByName : UITest
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
            var aviationEmail = "aviation@uat.co";
            var gymEmail = "gym@uat.co";
            var telephone = "02035070033";

            Run<Undischarged_AddCustomerEmailRecipient_Aviation_Gym>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            ClickLink("Customers");
            ExpectHeader("Customers");
            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Email Recipients").Click("2");

            WaitToSeeHeader($"[#{customerName}#] - Email Recipients");

            Set("Name").To(name);
            Click("Search");
            ExpectRow(aviationEmail);
            ExpectNoRow(gymEmail);
        }
    }
}