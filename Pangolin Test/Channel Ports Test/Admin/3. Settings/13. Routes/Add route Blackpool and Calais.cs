﻿using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class AddRouteBlackpoolAndCalais : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<AddNon_UKPortCalais>();
            LoginAs<ChannelPortsAdmin>();

            ExpectHeader(That.Contains, "Shipments");
            Click("Settings");

            Click("Routes");
            ExpectHeader(That.Contains, "Routes");

            Click("New Route");

            Set("Uk port").To("Blackpool");
            Set("Non-UK port").To("CALAIS");
            AtLabel("Manual").ClickLabel("Yes");

            Click("Save");

            ExpectHeader(That.Contains, "Routes");

            AtRow("Blackpool").Column("Uk port").Expect("Blackpool");
            AtRow("Blackpool").Column("Non-UK port").Expect("Calais");
        }
    }
}