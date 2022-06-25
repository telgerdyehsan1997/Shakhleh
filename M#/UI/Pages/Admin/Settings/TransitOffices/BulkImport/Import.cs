using MSharp;

namespace Admin.Settings.TransitOffices.BulkImport
{
    class ImportPage : SubPage<TransitOffices.BulkImportPage>
    {
        public ImportPage()
        {
            Add<Modules.TransitOfficeFileImportForm>();
            Layout(Layouts.FrontEndModal);
        }
    }
}