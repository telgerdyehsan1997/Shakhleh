using MSharp;

namespace Admin.Company.Product
{
    class BulkUploadPage : SubPage<ProductsPage>
    {
        public BulkUploadPage()
        {
            Add<Modules.ProductBulkUpload>();
            Layout(Layouts.FrontEndModal);
        }
    }
}