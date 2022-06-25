using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SupportTicketsList : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            //add shipment 1 on date X
            //("02/01/2020");
            Run<RiseTicketSupport>();

            LoginAs<ChannelPortsAdmin>();
            AtRow("R02220000001").Column("Actions").ExpectXPath("/html/body/main/div[1]/div/div/form/div[3]/div/div[2]/div/div[14]/div/button");
            ClickXPath("/html/body/main/div[1]/div/div/form/div[3]/div/div[2]/div/div[14]/div/button");
            ExpectLink("Raise Support Ticket");
            ClickLink("Raise Support Ticket");
        }
    }
}