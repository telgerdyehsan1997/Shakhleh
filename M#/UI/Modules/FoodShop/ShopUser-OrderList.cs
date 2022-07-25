using Domain;
using MSharp;
using ShopUser.Orders;

namespace Modules
{
    public class ShopUserOrderList : ListModule<Order>
    {
        public ShopUserOrderList()
        {
            HeaderText("سفارشات");

            DataSource("(await info.Shop.Orders.GetList())");

            DataSource("(await info.Shop.Orders.GetList())");
            Column(x => x.Customer).HeaderTemplate("مشتری");
            Column(x => x.Details).HeaderTemplate("جزئیات");
            Column(x => x.TotalPrice).HeaderTemplate("قیمت کل");

            Button("سفارش جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<EnterPage>().Send("shop", "info.Shop.ID").SendReturnUrl());

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

            ViewModelProperty<Domain.Shop>("Shop")
                .OnBound("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");

        }
    }
}
