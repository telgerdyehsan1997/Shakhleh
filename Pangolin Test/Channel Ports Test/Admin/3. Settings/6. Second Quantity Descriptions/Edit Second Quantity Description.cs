using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class EditSecondQuantityDescription : UITest
    {
        [TestProperty("Sprint", "1")]

        [PangolinTestMethod]
        public override void RunTest()
        {
            //Add new product and SQD
            Run<CreateNewSecondQuantityDescription_SquareMetres, AdminAddsProduct_IPad>();
            LoginAs<ChannelPortsAdmin>();

            Click("Settings");
            WaitToSeeHeader(That.Contains, "Users");
            Click("Second Quantity Descriptions");
            WaitToSeeHeader(That.Contains, "Second Quantity Descriptions");

            //edit SQD
            AtRow(That.Contains, "025").Column(That.Contains, "Edit").Click("Edit");
            Set("Quantity code").To("026");
            Set("Description").To("Square Centimetres");
            Click("Save");
            WaitToSeeHeader("Second Quantity Descriptions");

            //assert list updates
            ExpectRow(That.Contains, "026");
            AtRow(That.Contains, "026").Column("Quantity code").Expect("026");
            AtRow(That.Contains, "026").Column("Description").Expect("Square Centimetres");
            ExpectNoRow(That.Contains, "025");

            //assert Companies > [Name] > Products updates
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");
            WaitToSeeHeader("Products");
            AtRow(That.Contains, "ABS12343").Column("Second quantity").Expect("026");
        }
    }
}
