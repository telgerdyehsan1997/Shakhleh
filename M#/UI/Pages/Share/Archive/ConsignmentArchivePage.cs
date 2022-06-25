

using MSharp;

namespace Share.Archive
{
    class ConsignmentArchivePage : RootPage
    {
        public ConsignmentArchivePage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.ConsignmentArchiveConfirmation>();
        }
    }
}