using MSharp;

namespace Admin.Settings.Import
{
    class ErrorsPage : SubPage<Admin.Settings.ImportPage>
    {
        public ErrorsPage()
        {
            Add<Modules.ImportErrorList>();
        }
    }
}