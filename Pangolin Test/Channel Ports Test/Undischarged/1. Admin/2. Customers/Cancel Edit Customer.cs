using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_CancelEditCustomer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Undischarged_AddRecognisedCustomer>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            var customerAccountNumber = "Z9999";
            ClickLink("Customers");
            ExpectHeader("Customers");
            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Edit").Click("Edit");
            Set("Customer Account Number").To(customerAccountNumber);
            Click(customerAccountNumber);
            AtLabel("Email Recipients").ClickLabel("Send to contacts on this system as well as Customs Pro creator contacts.");
            Click("Save");

            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Customer Name").Expect("Channel Ports");
            AtRow(customerAccountNumber).Column("Address").Expect("Folkestone Services, Junction 11, M20, Hythe, CT21 4BL");
            AtRow(customerAccountNumber).Column("Country").Expect("GB");
        }
    }
}