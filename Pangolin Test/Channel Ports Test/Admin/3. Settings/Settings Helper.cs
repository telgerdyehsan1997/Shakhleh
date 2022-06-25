using Pangolin;

namespace Channel_Ports_Test
{
    public static class SettingsHelper
    {
        public static void NavigateToSettingsPage(this UITest @this, string settingsPage)
        {
            @this.ClickLink("Settings");
            @this.ExpectHeader("Users");
            @this.ClickLink(settingsPage);
            @this.ExpectHeader(settingsPage);
        }
    }
}