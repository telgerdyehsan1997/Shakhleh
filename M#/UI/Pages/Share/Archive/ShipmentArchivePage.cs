

using MSharp;

namespace Share.Archive
{
    class ShipmentArchivePage : RootPage
    {
        public ShipmentArchivePage()
        {
            Layout(Layouts.FrontEndModal);
            Add<Modules.ShipmentArchiveLogForm>();
        }
    }
}