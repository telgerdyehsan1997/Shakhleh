using Domain;
using MSharp;
using ShopUser.Settings.Discounts;

namespace Modules
{
    public class ShopUserDiscountList : ListModule<Discount>
    {
        public ShopUserDiscountList()
        {
            HeaderText("تخفیف ها");

            DataSource("(await info.Shop.Discounts.GetList())");

            this.ArchiveSearch();
            SearchButton("Search").Text("جستجو").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Name).HeaderTemplate("نام");
            Column(x => x.CalculationType).HeaderTemplate("نوع محاسبه");
            Column(x => x.Type).HeaderTemplate("نوع تخفیف");
            Column(x => x.IsFoodSpecific).HeaderTemplate("مخصوص غذا خاصی است؟");
            Column(x => x.IsUserSpecific).HeaderTemplate("مخصوص مشتری خاصی است؟");
            Column(x => x.MinimumAmountOfPriceToUse)
                .HeaderTemplate("حداقل میزان خرید برای استفاده از تخفیف");
            Column(x => x.MaximumAmountOfPriceToUse)
                .HeaderTemplate("حداکثر میزان خرید برای استفاده از تخفیف");

            ButtonColumn("ویرایش").HeaderText("ویرایش").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<EnterPage>().Send("item", "item.ID").Send("shop", "info.Shop.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Discount");

            Button("تخفیف جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<EnterPage>().Send("shop", "info.Shop.ID").SendReturnUrl());


            PagerPosition(PagerAt.Bottom);
            PageSize(10);

            ViewModelProperty<Domain.Shop>("Shop")
                .OnBound("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");

        }
    }
}
