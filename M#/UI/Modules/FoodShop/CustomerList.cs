using Domain;
using MSharp;
using Admin.Shops.Details.Customers;

namespace Modules
{
    public class CustomerList : ListModule<ShopCustomer>
    {
        public CustomerList()
        {
            HeaderText("مشتریان");

            DataSource("(await info.Shop.Customers.GetList())");

            this.ArchiveSearch();
            SearchButton("Search").Text("جستجو").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Name).HeaderTemplate("نام");
            Column(x => x.Email).HeaderTemplate("ایمیل");
            Column(x => x.Phone).HeaderTemplate("شماره تلفن");

            ButtonColumn("ویرایش").HeaderText("ویرایش").GridColumnCssClass("actions").Icon(FA.Edit)
                .OnClick(x => x.Go<EnterPage>().Send("item", "item.ID").Send("shop", "info.Shop.ID").SendReturnUrl());

            this.ArchiveButtonColumn("ShopCustomer");

            Button("مشتری جدید").Icon(FA.Plus)
                .OnClick(x => x.Go<EnterPage>().Send("shop", "info.Shop.ID").SendReturnUrl());

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

            ViewModelProperty<Domain.Shop>("Shop").FromRequestParam("shop");

        }
    }
}
