using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddNewNCTSShipment_IntoUK : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddAuthorisedLocationWarehouse1>();
            LoginAs<ChannelPortsAdmin>();

            Click("NCTS Shipments Out of UK");

            ClickLink(That.Contains, "New NCTS Shipment Out of UK");

            ExpectHeader("Shipment details");

            //Set("Company name").To("");

            //Set("authorised location").To("ware");
            //Click("Warehouse 1");
            //Set("Entry number").To("ABCDEFGHI123456789");
            //Set("Vehicle number").To("T37");
            //Click("Save and Transmit");

            //ExpectHeader(That.Contains, "NCTS Shipments Into UK");
            //Set("to").To("01/07/2020");
            //Click("Search");

            //ExpectRow("99MBABCDEFG1234567");
            //AtRow("99MBABCDEFG1234567").Column("Progress").Expect("Transmitted");
            //AtRow("99MBABCDEFG1234567").Column("Confirm Out Turn").ExpectNo("Confirm");
            //AtRow("99MBABCDEFG1234567").Column("Edit").ExpectNo("Edit");

            //AtRow("99MBABCDEFG1234567").Column("Date/ time of arrival").Expect(""); //info is coming from another source. 
            //AtRow("99MBABCDEFG1234567").Column("Date/ time of discharge").Expect(""); //info is coming from another source. 
            //AtRow("99MBABCDEFG1234567").Column("View error").Expect("");


        }
    }
}