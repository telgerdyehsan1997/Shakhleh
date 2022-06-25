using MSharp;

namespace Admin.Company.Product
{
    class EnterPage : SubPage<ProductsPage>
    {
        public EnterPage()
        {
            Add<Modules.ProductForm>();
        }
    }
}