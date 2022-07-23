using MSharp;
using Domain;

namespace ShopUser.Customers
{
    public class EnterPage : SubPage<CustomersPage>
    {
        public EnterPage()
        {
            Add<Modules.ShopUserCustomerForm>();
        }
    }
}