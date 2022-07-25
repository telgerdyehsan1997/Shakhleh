using MSharp;
using Domain;

namespace ShopUser.Orders
{
    public class EnterPage : SubPage<OrdersPage>
    {
        public EnterPage()
        {
            Add<Modules.ShopUserOrderForm>();
        }
    }
}