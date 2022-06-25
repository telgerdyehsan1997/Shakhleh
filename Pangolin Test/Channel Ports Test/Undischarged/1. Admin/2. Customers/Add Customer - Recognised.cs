using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_AddRecognisedCustomer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customerAccountNumber = "Z9999";

            LoginAs<Undischarged_ChannelPortsAdmin>();

            ClickLink("Customers");
            ExpectHeader("Customers");

            ClickLink("New Customer");
            ExpectHeader("Customer Details");
            Set("Customer Account Number").To(customerAccountNumber);

            ClickButton("Search");
            ExpectHeader("Customer Details");

            AtLabel("Email Recipients").ClickLabel("Send only to the contacts on this system");
            ClickButton("Save");

            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Customer Name").Expect("Channel Ports");
            AtRow(customerAccountNumber).Column("Address").Expect("Folkestone Services, Junction 11, M20, Hythe, CT21 4BL");
            AtRow(customerAccountNumber).Column("Country").Expect("United Kingdom");
        }
    }
}