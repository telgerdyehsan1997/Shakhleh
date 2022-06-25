using MSharp;

namespace Admin.Dashboard
{
    class BannerMessageFormPage : SubPage<SettingsPage>
    {
        public BannerMessageFormPage()
        {
            BrowserTitle("Banner Message > View");
            Roles(AppRole.Admin);
            Add<Modules.BannerMessageForm>();

        }
    }
}