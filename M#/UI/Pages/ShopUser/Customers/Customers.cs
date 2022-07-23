using MSharp;
using Domain;

namespace ShopUser.Customers
{
    public class CustomersPage : SubPage<ShopUser.CustomersPage>
    {
        public CustomersPage()
        {

            Add<Modules.ShopUserCustomerList>();
        }
    }
}