using MSharp;

namespace Admin.Settings.Import
{
    class UploadUNCodePage : SubPage<Admin.Settings.UNCodeImportsPage>
    {
        public UploadUNCodePage()
        {
            Layout(Layouts.FrontEndModal);

            Add<Modules.ImportUNCodesForm>();
        }
    }
}