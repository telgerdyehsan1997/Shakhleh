using MSharp;

namespace Admin.Company.BulkUpload
{
    class ErrorsPage : SubPage<Admin.Company.BulkUploadListPage>
    {
        public ErrorsPage()
        {
            Add<Modules.ImportErrorList>();
        }
    }
}