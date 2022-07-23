using MSharp;
using Domain;

namespace Admin.Shops
{
    public class CustomersPage : SubPage<ShopsPage>
    {
        public CustomersPage()
        {
            Set(PageSettings.LeftMenu, "AdminShopsMenu");

            Add<Modules.CustomerList>();
        }
    }
}