using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class ManageSecondQuantityDescriptions : UITest
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

            //assert SQD appears correct in list
            AtRow(That.Contains, "025").Column("Quantity code").Expect("025");
            AtRow(That.Contains, "025").Column("Description").Expect("Litres");
            //assert SQD appears correct in Products
            Click("Companies");
            WaitToSeeHeader("Companies");
            AtRow(That.Contains, "Truckers Ltd").Column("Company name").Click("Truckers Ltd");
            WaitToSeeHeader("Truckers Ltd");
            Click("Products");
            WaitToSeeHeader("Products");
            AtRow(That.Contains, "ABS12343").Column("Second quantity").Expect("025");

            //assert list layout
            Click("Settings");
            WaitToSeeHeader("Users");
            Click("Second Quantity Descriptions");
            WaitToSeeHeader("Second Quantity Descriptions");
            RightOfHeader("Second Quantity Descriptions").Expect("New Second Quantity Description");
            BelowHeader("Second Quantity Descriptions").Expect("Status");
            RightOf("Status").ExpectButton("Search");

            //assert 'edit item' layout
            Click("New Second Quantity Description");
            BelowHeader("Second Quantity Description Details").ExpectField("Quantity code");
            BelowField("Quantity code").ExpectField("Description");
            BelowField("Description").ExpectButton("Cancel");
            NearButton("Cancel").ExpectButton("Save");
            Click("Cancel");
            WaitToSeeHeader("Second Quantity Descriptions");

            //test search while new item is NOT archived
            ClickLabel("All");
            Click("Search");
            AtRow(That.Contains, "025").Column(That.Contains, "Quantity code").Expect("025");
            AtRow(That.Contains, "025").Column(That.Contains, "Description").Expect("Litres");
            AtRow(That.Contains, "025").Column(That.Contains, "Archive").Expect("Archive");
            ClickLabel("Active");
            Click("Search");
            AtRow(That.Contains, "025").Column(That.Contains, "Quantity code").Expect("025");
            AtRow(That.Contains, "025").Column(That.Contains, "Description").Expect("Litres");
            AtRow(That.Contains, "025").Column(That.Contains, "Archive").Expect("Archive");
            ClickLabel("Archived");
            Click("Search");
            ExpectNoRow(That.Contains, "025");

            //test search by archiving SQD
            ClickLabel("All");
            Click("Search");
            AtRow(That.Contains, "025").Column("Archive").Click("Archive");
            ExpectHeader("Archive");
            Set("Please Explain Why").To("Archive Reason");
            ClickButton("Archive");
            WaitToSeeHeader("Second Quantity Descriptions");
            AtRow(That.Contains, "025").Column(That.Contains, "Quantity code").Expect("025");
            AtRow(That.Contains, "025").Column(That.Contains, "Description").Expect("Litres");
            AtRow(That.Contains, "025").Column(That.Contains, "Archive").Expect("Unarchive");
            ClickLabel("Active");
            Click("Search");
            ExpectNoRow(That.Contains, "025");
            ClickLabel("Archived");
            Click("Search");
            AtRow(That.Contains, "025").Column(That.Contains, "Quantity code").Expect("025");
            AtRow(That.Contains, "025").Column(That.Contains, "Description").Expect("Litres");
            AtRow(That.Contains, "025").Column(That.Contains, "Archive").Expect("Unarchive");
        }
    }
}
