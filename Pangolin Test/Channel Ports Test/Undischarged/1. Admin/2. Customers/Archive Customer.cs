using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_ArchiveCustomer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<Undischarged_AddRecognisedCustomer>();
            LoginAs<Undischarged_ChannelPortsAdmin>();

            var customerAccountNumber = "Z9999";
            ClickLink("Customers");
            WaitToSeeHeader("Customers");
            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Archive").ClickLink("");
            ExpectHeader("Archive");
            ExpectField("Please Explain Why");
            ClickButton("Cancel");

            AtRow(customerAccountNumber).Column("Archive").ClickLink("");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            WaitToSeeHeader("Customers");
        }
    }
}