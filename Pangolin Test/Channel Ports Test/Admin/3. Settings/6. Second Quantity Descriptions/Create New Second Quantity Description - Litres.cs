using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class CreateNewSecondQuantityDescription_Litres : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //login as admin
            LoginAs<ChannelPortsAdmin>();
            ClickLink("Settings");
            WaitToSeeHeader("Users");
            Click("Second Quantity Descriptions");
            WaitToSeeHeader("Second Quantity Descriptions");
            Click("New Second Quantity Description");
            WaitToSeeHeader(That.Contains, "Second Quantity Description Details");

            //assert layout
            BelowHeader("Second Quantity Description Details").ExpectField("Quantity code");
            BelowField("Quantity code").ExpectField("Description");
            BelowField("Description").ExpectButton("Cancel");
            NearButton("Cancel").ExpectButton("Save");

            //create new SQD
            Set("Quantity code").To("89");
            Set("Description").To("Litres");
            Click("Save");

            //assert details in list
            WaitToSeeHeader("Second Quantity Descriptions");
            AtRow(That.Contains, "89").Column(That.Contains, "Quantity code").Expect(What.Contains, "89");
            AtRow(That.Contains, "89").Column(That.Contains, "Description").Expect(What.Contains, "Litres");
        }
    }
}