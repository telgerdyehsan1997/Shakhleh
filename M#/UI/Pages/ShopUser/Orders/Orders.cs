using MSharp;
using Domain;

namespace ShopUser.Orders
{
    public class OrdersPage : SubPage<ShopUser.OrdersPage>
    {
        public OrdersPage()
        {

            Add<Modules.ShopUserCustomerList>();
        }
    }
}