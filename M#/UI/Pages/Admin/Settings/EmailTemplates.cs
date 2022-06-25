using MSharp;

namespace Admin.Settings
{
    class EmailTemplatesPage : SubPage<Admin.SettingsPage>
    {
        public EmailTemplatesPage()
        {
            Add<Modules.EmailTemplateList>();
        }
    }
}