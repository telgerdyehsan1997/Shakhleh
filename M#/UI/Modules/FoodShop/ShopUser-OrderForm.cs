using Domain;
using MSharp;
using ShopUser.Orders;

namespace Modules
{
    public class ShopUserOrderForm : FormModule<Order>
    {
        public ShopUserOrderForm()
        {
            HeaderText("سفارش");

            MasterDetail(x => x.FoodItems, s => {
                s.HeaderText("آیتم های سفارش");
                s.Field(y => y.Food).HeaderText("غذا")
                .ReloadOnChange()
                .DataSource("(await info.Shop.Foods.GetList())")
                .AsAutoComplete();
                s.Field(y => y.Count).HeaderText("تعداد");
                s.CustomField("UnitPrice").ControlMarkup("@Model.Food?.Price").Label("قیمت هر عدد");

                s.Button("افزودن آیتم").Icon(FA.Plus).OnClick(x => x.AddMasterDetailRow());
            }).Label("آیتم های سفارش").MinCardinality(1) ;

            AutoSet(x => x.Shop).Value("info.Shop");

            Button("ذخیره").OnClick(x =>
            {
                x.SaveInDatabase();
                x.CSharp("applying discount code")
                .Code(@"var discount = await info.Item.Shop.GetActiveDiscount();
                if (discount!=null)
                {
                    info.Item.UsedDiscountId = discount.ID;
                    await info.Item.ApplyDiscount(discount);
                }");
                x.GentleMessage("ذخیره شد.");
                x.Go<FactorOverviewPage>().Send("order", "info.Item.ID");
            });
            Button("لغو").OnClick(x =>x.ReturnToPreviousPage());
            ViewModelProperty<Domain.Shop>("Shop")
                .OnBound("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");

            OnBeforeSave("Binding Shop")
                .Code("item.Shop = info.Shop;");
            OnBeforeSave("Binding Details")
                .Code(@"item.Details = string.Join(',', (info.FoodItems.ToList()).Select(x => $"" {x.Food.Name} * {x.Count} ""));");
            

        }
    }
}
