using MSharp;

namespace Admin.Settings.Import
{
    class RowQuotaErrorLogPage : SubPage<Admin.Settings.RowQuotaImportsPage>
    {
        public RowQuotaErrorLogPage()
        {
            Add<Modules.RowQuotasImportErrorList>();
        }
    }
}