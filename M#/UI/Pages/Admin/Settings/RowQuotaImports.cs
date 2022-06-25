using MSharp;

namespace Admin.Settings
{
    class RowQuotaImportsPage : SubPage<SettingsPage>
    {
        public RowQuotaImportsPage()
        {
            Add<Modules.ImportRowQuotasList>();
        }
    }
}
