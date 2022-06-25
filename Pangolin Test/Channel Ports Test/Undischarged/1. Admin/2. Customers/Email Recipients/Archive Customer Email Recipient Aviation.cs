using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_ArchiveCustomerEmailRecipient_Aviation : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Undischarged_AddCustomerEmailRecipient_Aviation>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            var customerAccountNumber = "Z9999";
            var email = "test1@uat.co";
            var customerName = "Channel Ports";

            ClickLink("Customers");
            ExpectHeader("Customers");
            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Email Recipients").Click("1");
            WaitToSeeHeader($"[#{customerName}#] - Email Recipients");

            ExpectRow(email);
            AtRow(email).Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            ClickButton("Cancel");

            AtRow(email).Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            WaitToSeeHeader($"[#{customerName}#] - Email Recipients");
        }
    }
}