using Domain;
using MSharp;
using UI.Pages.Admin.FoodShops;

namespace Modules
{
    public class FoodShopList : ListModule<FoodShop>
    {
        public FoodShopList()
        {
            HeaderText("مغازه ها");


            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Name).HeaderTemplate("نام");
            Column(x => x.Description).HeaderTemplate("توضیحات");
            Column(x => x.Address).HeaderTemplate("آدرس");
            Column(x => x.Email).HeaderTemplate("ایمیل");
            Column(x => x.Phone).HeaderTemplate("شماره تلفن");

            ButtonColumn("ویرایش").HeaderText("ویرایش").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<Admin.FoodShops.EnterPage>().Send("item", "item.ID").SendReturnUrl());
            this.ArchiveButtonColumn("SocialMedia");

            Button("مغازه جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<Admin.FoodShops.EnterPage>().SendReturnUrl());

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

        }
    }
}
