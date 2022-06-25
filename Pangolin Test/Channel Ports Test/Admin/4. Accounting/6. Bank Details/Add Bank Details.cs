using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddBankDetails : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();
            Click("Accounting");
            Click("Bank Details");
            ExpectHeader("Bank Details");

            Set("Bankers").To(@"GBP Sterling
Nat West Bank
City of London Office
PO Box 00000023
1 Some Street
London EC2A ABC");
            Set("Sort Code").To("23-23-23");
            Set("Account no").To("00000000");
            Set("IBAN").To("GB99 MNDK 9000 L0293 2039 23");
            Set("BIC").To("LDKF KSLD");
            Click("Save");


        }
    }
}