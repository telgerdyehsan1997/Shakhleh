using MSharp;

namespace Admin.Settings.TransitOffices.BulkImport
{
    class ErrorsPage : SubPage<TransitOffices.BulkImportPage>
    {
        public ErrorsPage()
        {
            Add<Modules.TransitOfficeFileErrorList>();
        }
    }
}