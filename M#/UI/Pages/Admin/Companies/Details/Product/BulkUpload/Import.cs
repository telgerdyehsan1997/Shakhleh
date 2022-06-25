using MSharp;

namespace Admin.Company.Product
{
    class BulkUploadListPage : SubPage<Admin.Company.ProductsPage>
    {
        public BulkUploadListPage()
        {
            Add<Modules.ProductBulkUploadList>();
        }
    }
}