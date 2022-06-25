﻿using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class Shipments_AssertSearchFiltersSaveWhenNavigatingAwayFromPage : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddShipmentIntoUKASMAccepted>();
            LoginAs<ChannelPortsAdmin>();

            //Sets the search filters
            Set("Tracking number").To("R0721000001");
            Set("Port of arrival/departure").To("Portsmouth");
            Set("Company name").To("GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            Set("Trailer number").To("1234567");
            Set("Date created").To("01/07/2021");
            Set("Expected date of arrival/departure").To("19/01/2023");
            Set("Customer Reference").To("R123");
            Set("Vehicle number").To("RAF123");
            Set("Progress").To("Ready to Transmit");
            ClickButton("Search");
            ExpectRow("R0721000001");

            //Navigates away from Shipment page
            ClickLink("Accounting");
            ExpectHeader("VAT Rates");
            ClickLink("Settings");
            ExpectHeader("Users");

            //Navigates back to Shipment page
            ClickLink("Shipments");
            ExpectHeader("Shipments");
            //Asserts that filters have saved
            AtField("Tracking number").Expect("R0721000001");
            AtField("Port of arrival/departure").Expect("Portsmouth");
            AtField("Company name").Expect("GEEKSQA - WORCESTER - WR5 3DA - GB683470514001 - 7654321");
            AtField("Trailer number").Expect("1234567");
            AtLabel("Date created").Expect("01/07/2021");
            AtLabel("Expected date of arrival/departure").Expect("19/01/2023");
            AtField("Customer Reference").Expect("R123");
            AtField("Vehicle number").Expect("RAF123");
            AtLabel("Progress").Expect("Ready to Transmit");
        }
    }
}