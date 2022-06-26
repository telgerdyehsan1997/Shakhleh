using Olive;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public static class AppSetting
    {
        public static string ChannelPort => Config.Get<string>("Integration:ChannelPort:Root");

    }
}
