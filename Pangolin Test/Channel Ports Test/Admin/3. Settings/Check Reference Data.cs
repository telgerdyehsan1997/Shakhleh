using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CheckReferenceData : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            LoginAs<ChannelPortsAdmin>();

            ClickLink("Settings");

            //offices of transit

            Click("Offices of Transit");
            AtRow("GB000060").Column("NCTS Code").Expect("GB000060");
            AtRow("GB000060").Column("Usual name").Expect("Dover/Folkestone Eurotunnel Freight");
            AtRow("GB000060").Column("Departure").ExpectTick();
            AtRow("GB000060").Column("Destination").ExpectTick();
            AtRow("GB000060").Column("Transit").ExpectTick();
            AtRow("GB000060").Click("Edit");
            //NearLabel("Geo location").ExpectValue("Atlantic coast"); Not implemented

            //authorised location

            Click("Authorised Locations");

            AtRow("Stop 24").Column("Location name").Expect("Stop 24");
            AtRow("Stop 24").Column("Customs identity").Expect("24FOLK CT21 4BL");
            AtRow("Stop 24").Column("NCTS code").Expect("GB000060");
            AtRow("Stop 24").Column("Authorisation number").Expect("24FOLK CT21 4BL");

            //Port

            Click("Ports");

            AtRow("Portsmouth").Column("Port name").Expect("Portsmouth");
            AtRow("Portsmouth").Column("Port code").Expect("PTM");
            AtRow("Portsmouth").Column("Non-UK").ExpectNoTick();
            AtRow("Portsmouth").Column("NCTS code").Expect("GB000060");
            AtRow("Portsmouth").Column("UKBF email").Expect("leit.portsmouth@homeoffice.gov.uk");
        }
    }
}