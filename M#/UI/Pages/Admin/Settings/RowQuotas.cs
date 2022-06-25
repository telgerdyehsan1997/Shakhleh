using MSharp;

namespace Admin.Settings
{
    class RowQuotasPage : SubPage<Admin.SettingsPage>
    {
        public RowQuotasPage()
        {
            Add<Modules.RowQuotasList>();
        }
    }
}