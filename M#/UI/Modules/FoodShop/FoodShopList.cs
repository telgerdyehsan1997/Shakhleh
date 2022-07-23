using Domain;
using MSharp;
using Admin.FoodShops;

namespace Modules
{
    public class FoodShopList : ListModule<FoodShop>
    {
        public FoodShopList()
        {
            HeaderText("مغازه ها");


            this.ArchiveSearch();
            SearchButton("Search").Icon(FA.Search).OnClick(x => x.ReturnView());

            LinkColumn(x => x.Name)
                .HeaderText("نام")
                .OnClick(x => x.Go<DetailsPage>().Send("shop", "item.ID"));

            Column(x => x.Description).HeaderTemplate("توضیحات");
            Column(x => x.Address).HeaderTemplate("آدرس");
            Column(x => x.Email).HeaderTemplate("ایمیل");
            Column(x => x.Phone).HeaderTemplate("شماره تلفن");

            ButtonColumn("ویرایش").HeaderText("ویرایش").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<EnterPage>().Send("item", "item.ID").SendReturnUrl());
            this.ArchiveButtonColumn("SocialMedia");

            Button("مغازه جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<EnterPage>().SendReturnUrl());

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

        }
    }
}
