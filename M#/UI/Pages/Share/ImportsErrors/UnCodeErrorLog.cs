using MSharp;

namespace Admin.Settings.Import
{
    class UnCodeErrorLogPage : SubPage<Admin.Settings.UNCodeImportsPage>
    {
        public UnCodeErrorLogPage()
        {
            Add<Modules.UNCodesImportErrorList>();
        }
    }
}