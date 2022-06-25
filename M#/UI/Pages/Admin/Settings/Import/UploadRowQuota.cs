using MSharp;

namespace Admin.Settings.Import
{
    class UploadRowQuotaPage : SubPage<Admin.Settings.RowQuotaImportsPage>
    {
        public UploadRowQuotaPage()
        {
            Layout(Layouts.FrontEndModal);

            Add<Modules.ImportRowQuotaForm>();
        }
    }
}