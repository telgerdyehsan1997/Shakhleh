using MSharp;

namespace Share.Archive
{
    class ArchivePopUpPage : RootPage
    {
        public ArchivePopUpPage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.ArchiveLogForm>();
        }
    }
}