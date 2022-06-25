using MSharp;

namespace Admin.Company
{
    class ProductsPage : SubPage<CompaniesPage>
    {
        public ProductsPage()
        {
            Set(PageSettings.LeftMenu, "CompanyMenu");
            Add<Modules.ProductList>();
        }
    }
}