using MSharp;

namespace Admin.Settings
{
    class FileErroLogPage : SubPage<Admin.SettingsPage>
    {
        public FileErroLogPage()
        {
            Add<Modules.ASMFileErrorLogList>();

        }
    }
}