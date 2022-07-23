using Domain;
using MSharp;
using Admin.Shops.Details.Users;

namespace Modules
{
    public class OrderList : ListModule<Order>
    {
        public OrderList()
        {
            HeaderText("سفارشات");

            DataSource("(await info.Shop.Orders.GetList())");
            Column(x => x.Customer);
            Column(x => x.TotalPrice);


            PagerPosition(PagerAt.Bottom);
            PageSize(10);

            ViewModelProperty<Domain.Shop>("Shop").FromRequestParam("shop");

        }
    }
}
