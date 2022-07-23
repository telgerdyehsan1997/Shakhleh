using Domain;
using MSharp;
using Admin.Shops;

namespace Modules
{
    public class ShopList : ListModule<Shop>
    {
        public ShopList()
        {
            HeaderText("مغازه ها");


            this.ArchiveSearch();
            SearchButton("Search").Text("جستجو").Icon(FA.Search).OnClick(x => x.ReturnView());

            LinkColumn(x => x.Name)
                .HeaderText("نام")
                .OnClick(x => x.Go<OverviewPage>().Send("shop", "item.ID"));

            Column(x => x.Description).HeaderTemplate("توضیحات");
            Column(x => x.Address).HeaderTemplate("آدرس");
            Column(x => x.Email).HeaderTemplate("ایمیل");
            Column(x => x.Phone).HeaderTemplate("شماره تلفن");

            ButtonColumn("ویرایش").HeaderText("ویرایش").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<EnterPage>().Send("item", "item.ID").SendReturnUrl());
            this.ArchiveButtonColumn("Shop");

            Button("مغازه جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<EnterPage>().SendReturnUrl());

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

        }
    }
}
