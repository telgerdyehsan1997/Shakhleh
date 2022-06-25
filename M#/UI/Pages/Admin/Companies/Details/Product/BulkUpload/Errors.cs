using MSharp;

namespace Admin.Company.Product.BulkUpload
{
    class ErrorsPage : SubPage<Admin.Company.Product.BulkUploadListPage>
    {
        public ErrorsPage()
        {
            Add<Modules.ImportErrorList>();
        }
    }
}