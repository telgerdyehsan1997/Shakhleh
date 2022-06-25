using MSharp;

namespace Admin.Settings.TransitOffices
{
    class BulkImportPage : SubPage<Settings.TransitOfficesPage>
    {
        public BulkImportPage()
        {
            Add<Modules.TransitOfficeFileImportList>();
        }
    }
}