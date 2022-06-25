using MSharp;

namespace Admin.Settings
{
    class DefaultTermsOfSalePage : SubPage<Admin.SettingsPage>
    {
        public DefaultTermsOfSalePage()
        {
            Add<Modules.TermsOfSaleList>();
        }
    }
}