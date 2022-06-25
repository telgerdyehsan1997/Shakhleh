using Pangolin;
using System;

namespace Channel_Ports_Test
{
    public static class RoutesHelper
    {
        public static void navigateToRoutes(this UITest @this)
        {
            @this.ClickLink("Settings");
            @this.ExpectHeader("Users");
            @this.ClickLink("Routes");
            @this.ExpectHeader("Routes");
        }
    }
}