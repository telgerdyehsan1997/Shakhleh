using Pangolin;

namespace Channel_Ports_Test
{
    public static class UDSettingsHelper
    {
        public static void NavigateToUDSettings(this UITest @this, string settingsPage)
        {
            @this.ClickLink("Settings");
            @this.ExpectLink(settingsPage);
            @this.ClickLink(settingsPage);
            @this.ExpectHeader(settingsPage);
        }
    }
}