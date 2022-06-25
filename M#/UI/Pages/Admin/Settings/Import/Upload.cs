using MSharp;

namespace Admin.Settings.Import
{
    class UploadPage : SubPage<Admin.Settings.ImportPage>
    {
        public UploadPage()
        {
            Layout(Layouts.FrontEndModal);

            Add<Modules.ImportQueueItemForm>();
        }
    }
}