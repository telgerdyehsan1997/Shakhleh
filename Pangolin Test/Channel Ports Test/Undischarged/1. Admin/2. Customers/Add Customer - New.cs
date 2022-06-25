using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Undischarged_AddNewCustomer : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            var customerAccountNumber = "Z1111";
            var customerName = "Z1111 Name";
            var addressLine1 = "Z1111 address 1";
            var addressLine2 = "Z1111 address 2";
            var city = "Z1111 city";
            var postcode = "Z1111 postcode";
            var country = "Spain";
            var address = new string[] { addressLine1, addressLine2, city, postcode }.Trim().ToString(", ");

            LoginAs<Undischarged_ChannelPortsAdmin>();

            ClickLink("Customers");
            ExpectHeader("Customers");

            ClickLink("New Customer");
            ExpectHeader("Customer Details");
            Set("Customer Account Number").To(customerAccountNumber);

            //ClickLink("Search");
            ClickButton("Search");
            ExpectHeader("Customer Details");

            Set("Customer Name").To(customerName);
            Set("Address Line 1").To(addressLine1);
            Set("Address Line 2").To(addressLine2);
            Set("City").To(city);
            Set("Postcode").To(postcode);
            Set("Country").To(country);
            AtLabel("Email Recipients").ClickLabel("Send only to the contacts on this system");

            ClickButton("Save");

            ExpectRow(customerAccountNumber);
            AtRow(customerAccountNumber).Column("Customer Name").Expect(customerName);
            AtRow(customerAccountNumber).Column("Address").Expect(address);
            AtRow(customerAccountNumber).Column("Country").Expect(country);
        }
    }
}