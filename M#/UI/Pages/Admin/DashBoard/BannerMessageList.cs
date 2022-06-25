using MSharp;

namespace Admin.Dashboard
{
    class BannerMessageListPage : SubPage<SettingsPage>
    {
        public BannerMessageListPage()
        {
            BrowserTitle("Banner Message > View");
            Roles(AppRole.Admin);
            Add<Modules.BannerMessageList>();

        }
    }
}