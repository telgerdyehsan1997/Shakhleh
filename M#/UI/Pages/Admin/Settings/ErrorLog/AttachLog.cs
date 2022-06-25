using MSharp;
namespace Admin.Settings.ErrorLog
{
    class AttachLogPage : RootPage
    {
        public AttachLogPage()
        {
            Add<Modules.AttachASMFileSearch>();
            Add<Modules.AttachASMFileConsignmentList>();
        }
    }
}