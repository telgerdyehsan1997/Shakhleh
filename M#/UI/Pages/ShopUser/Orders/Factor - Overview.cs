using MSharp;
using Domain;

namespace ShopUser.Orders
{
    public class FactorOverviewPage : SubPage<OrdersPage>
    {
        public FactorOverviewPage()
        {
            Add<Modules.FactorOverview>();
        }
    }
}