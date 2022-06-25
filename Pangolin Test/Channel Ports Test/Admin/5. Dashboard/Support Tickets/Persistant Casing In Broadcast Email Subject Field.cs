using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class PersistantCasingInBroadcastEmailSubjectField : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            // ----------------------------------------------

            ClickLink("Dashboard");
            ExpectHeader("Support Tickets");
            ClickLink("Broadcast Message");
            ExpectHeader("Broadcast message details");

            // ----------------------------------------------

            //edit details
            Set("Subject").To("test SUBJECT");
            // ----------------------------------------------

            AtField("Subject").ExpectValue("test SUBJECT", Casing.Exact);
        }

    }
}