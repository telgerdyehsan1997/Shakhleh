using MSharp;

namespace Admin.Settings
{
    class DefaultTaxLinePage : SubPage<Admin.SettingsPage>
    {
        public DefaultTaxLinePage()
        {
            Add<Modules.DefaultTaxLineList>();
        }
    }
}