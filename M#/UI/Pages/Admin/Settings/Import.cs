using MSharp;

namespace Admin.Settings
{
    class ImportPage : SubPage<Admin.SettingsPage>
    {
        public ImportPage()
        {
            Add<Modules.ImportQueueItemList>();
        }
    }
}