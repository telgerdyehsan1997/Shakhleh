﻿using Pangolin;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Channel_Ports_Test
{
    [TestClass]
    public class SetCFSPMonthlyRepostRecipients : UITest
    {
        [PangolinTestMethod]
        public override void RunTest()
        {
            Run<SetUCN>();
            LoginAs<ChannelPortsAdmin>();

            //Navigates to Settings
            ClickLink("Settings");
            ExpectHeader("Users");

            //Navigates to Global Settings
            ClickLink("Global Settings");
            ExpectHeader("Global Settings");

            //Sets the 'CFSP Monthly Report Recipients'
            Set("CFSP Monthly Report Recipients").To("ADMIN@UAT.CO");
            ClickButton("Save");
            ExpectHeader("Global Settings");
        }
    }
}