using Domain;
using MSharp;

namespace Modules
{
    public class ShopUserDiscountForm : FormModule<Discount>
    {
        public ShopUserDiscountForm()
        {
            HeaderText("تخفیف");

            Field(x => x.Name).Label("نام");
            Field(x => x.Description).Label("توضیحات");

            Field(x => x.CalculationType)
                .ReloadOnChange()
                .Label("نوع محاسبه");

            Field(x => x.Percent)
                .Label("درصد تخفیف")
                .VisibleIf("info.CalculationType == DiscountCalculationType.Percentage");
            Field(x => x.Amount)
                .Label("میزان تخفیف")
                .VisibleIf("info.CalculationType==DiscountCalculationType.Amount");

            CustomField("MinimumAmountOfPriceToUseEnabled")
                .Label("تعیین حداقل میزان خرید؟")
                .PropertyType("bool")
                .ReloadOnChange()
                .PropertyName("MinimumAmountOfPriceToUseEnabled")
                .AsCheckBox();
            Field(x => x.MinimumAmountOfPriceToUse)
                .VisibleIf("info.MinimumAmountOfPriceToUseEnabled")
                .Label("حداقل میزان خرید برای استفاده از تخفیف");

            CustomField("MaximumAmountOfPriceToUseEnabled")
                .Label("تعیین حداکثر میزان خرید؟")
                .ReloadOnChange()
                .PropertyType("bool")
                .PropertyName("MaximumAmountOfPriceToUseEnabled")
                .AsCheckBox();
            Field(x => x.MaximumAmountOfPriceToUse)
                .VisibleIf("info.MaximumAmountOfPriceToUseEnabled")
                .Label("حداکثر میزان خرید برای استفاده از تخفیف");


            //Field(x => x.IsUserSpecific).ReloadOnChange().Label("مخصوص کاربر خاصی است؟");
            //Field(x => x.DiscountReceivers)
            //    .DataSource("(await info.Item.Users)")
            //    .VisibleIf("info.IsUserSpecific")
            //    .AsListbox().Label("مشتریان مشمول تخفیف");

            Field(x => x.FoodType)
                .ReloadOnChange()
                .AsRadioButtons(Arrange.Horizontal)
                .Mandatory()
                .Label("نوع غذا");

            Field(x => x.DiscountedFoods)
                .VisibleIf("info.FoodType.IsAnyOf(DiscountFoodType.OnlySpecifiedFoods)")
                .AsListbox().Label("غذاهای مورد تخفیف");

            Field(x => x.ExcludedFoods)
                .VisibleIf("info.FoodType.IsAnyOf(DiscountFoodType.AllFoodsButThereIsExclusion)")
                .AsListbox().Label("غذاهای بدون تخفیف");

            Field(x => x.Start).Label("تاریخ شروع اعتبار");
            Field(x => x.End).Label("تاریخ پایان اعتبار");


            AutoSet(x => x.Shop).Value("info.Shop");

            Button("ذخیره").OnClick(x =>
            {
                x.SaveInDatabase();
                x.CSharp(@"
                    await Database.Delete(await info.Item.ExcludedFoodsLinks.GetList());
                    await Database.Save(info.ExcludedFoods.Select(x=>new DiscountExcludedFoodsLink { FoodId=x.ID,DiscountId=info.Item.ID}));
                    await Database.Delete(await info.Item.DiscountedFoodsLinks.GetList());
                await Database.Save(info.DiscountedFoods.Select(x => new DiscountDiscountedFoodsLink { FoodId = x.ID, DiscountId = info.Item.ID }));");
                x.GentleMessage("ذخیره شد.");
                x.ReturnToPreviousPage();
            });
            Button("لغو").OnClick(x =>x.ReturnToPreviousPage());
            ViewModelProperty<Domain.Shop>("Shop")
                .OnBound("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");


            OnBeforeSave("Binding Shop")
                .Code("item.Shop = info.Shop;");


            OnPostBound("Binding Food Options")
                .Code(@"
            info.Shop=Context.Current.User().Extract<ShopUser>().Shop;
            info.DiscountedFoods_Options.Clear();
            info.DiscountedFoods_Options.Add(new EmptyListItem());
            info.DiscountedFoods_Options.AddRange(await info.Shop.Foods.GetList());

            info.ExcludedFoods_Options.Clear();
            info.ExcludedFoods_Options.Add(new EmptyListItem());
            info.ExcludedFoods_Options.AddRange(await info.Shop.Foods.GetList());
            ");

            OnBound_GET("Binding Foods")
                .Code(@"
                if (!info.Item.IsNew){
            info.ExcludedFoods = (await info.Item.ExcludedFoods).ToList();
            info.DiscountedFoods = (await info.Item.DiscountedFoods).ToList();

}");
        }
    }
}
