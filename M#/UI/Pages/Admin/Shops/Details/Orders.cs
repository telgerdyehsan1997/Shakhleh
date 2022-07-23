using MSharp;
using Domain;

namespace Admin.Shops
{
    public class OrdersPage : SubPage<ShopsPage>
    {
        public OrdersPage()
        {
            Set(PageSettings.LeftMenu, "AdminShopsMenu");

            Add<Modules.OrderList>();
        }
    }
}