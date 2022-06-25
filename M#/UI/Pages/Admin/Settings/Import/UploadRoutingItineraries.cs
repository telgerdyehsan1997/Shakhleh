using MSharp;

namespace Admin.Settings.Import
{
    class UploadRoutingItinerariesPage : SubPage<Admin.Settings.RoutingItinerariesImportPage>
    {
        public UploadRoutingItinerariesPage()
        {
            Layout(Layouts.FrontEndModal);

            Add<Modules.ImportRoutingItineraryForm>();
        }
    }
}