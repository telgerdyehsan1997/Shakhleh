using Domain;
using MSharp;
using ShopUser.Settings.Users;

namespace Modules
{
    public class ShopUserUserList : ListModule<Domain.ShopUser>
    {
        public ShopUserUserList()
        {
            HeaderText("کاربران");

            DataSource("(await info.Shop.Users.GetList())");

            this.ArchiveSearch();
            SearchButton("Search").Text("جستجو").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Name).HeaderTemplate("نام");
            Column(x => x.Email).HeaderTemplate("ایمیل");
            Column(x => x.Phone).HeaderTemplate("شماره تلفن");
            Column(x => x.IsAdmin).HeaderTemplate("ادمین");

            ButtonColumn("ویرایش").HeaderText("ویرایش").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<EnterPage>().Send("item", "item.ID").Send("shop", "info.Shop.ID").SendReturnUrl());

            this.ArchiveButtonColumn("ShopUser");

            Button("کاربر جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<EnterPage>().Send("shop", "info.Shop.ID").SendReturnUrl());

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

            ViewModelProperty<Domain.Shop>("Shop")
                .OnBound("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");

        }
    }
}
