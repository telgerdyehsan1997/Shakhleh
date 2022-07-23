using Domain;
using MSharp;
using ShopUser.Settings.Foods;

namespace Modules
{
    public class ShopUserFoodList : ListModule<Food>
    {
        public ShopUserFoodList()
        {
            HeaderText("غذاها");

            DataSource("(await info.Shop.Foods.GetList())");

            this.ArchiveSearch();
            SearchButton("Search").Text("جستجو").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Name).HeaderTemplate("نام");
            Column(x => x.Description).HeaderTemplate("توضیحات");
            Column(x => x.Price).HeaderTemplate("آدرس");
            Column(x => x.Image).HeaderTemplate("ایمیل");

            ButtonColumn("ویرایش").HeaderText("ویرایش").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<EnterPage>().Send("item", "item.ID").Send("shop", "info.Shop.ID").SendReturnUrl());

            this.ArchiveButtonColumn("Food");

            Button("غذای جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<EnterPage>().Send("shop", "info.Shop.ID").SendReturnUrl());

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

            ViewModelProperty<Domain.Shop>("Shop")
                .OnBound("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");

        }
    }
}
