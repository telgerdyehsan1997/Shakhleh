using Domain;
using MSharp;
using Admin.Shops.Details.Users;

namespace Modules
{
    public class DiscountList : ListModule<Discount>
    {
        public DiscountList()
        {
            HeaderText("تخفیف ها");

            DataSource("(await info.Shop.Discounts.GetList())");

            this.ArchiveSearch();
            SearchButton("Search").Text("جستجو").Icon(FA.Search).OnClick(x => x.ReturnView());

            Column(x => x.Name).HeaderTemplate("نام");
            Column(x => x.CalculationType).HeaderTemplate("نوع محاسبه");
            Column(x => x.FoodType).HeaderTemplate("نوع تخفیف");
            Column(x => x.IsUserSpecific).HeaderTemplate("مخصوص مشتری خاصی است؟");


            this.ArchiveButtonColumn("Discount");

            PagerPosition(PagerAt.Bottom);
            PageSize(10);

            ViewModelProperty<Domain.Shop>("Shop").FromRequestParam("shop");

        }
    }
}
