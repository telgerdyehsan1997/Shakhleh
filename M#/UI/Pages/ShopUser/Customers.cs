using MSharp;
using Domain;

namespace ShopUser
{
    public class CustomersPage : SubPage<ShopUserPage>
    {
        public CustomersPage()
        {

            OnStart(x => x.Go<Customers.CustomersPage>().RunServerSide());
        }
    }
}