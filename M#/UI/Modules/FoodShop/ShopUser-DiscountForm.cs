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


            Field(x => x.IsUserSpecific).ReloadOnChange().Label("مخصوص کاربر خاصی است؟");
            Field(x => x.DiscountReceivers)
                .VisibleIf("info.IsUserSpecific")
                .AsListbox().Label("مشتریان مشمول تخفیف");

            Field(x => x.IsFoodSpecific).ReloadOnChange().Label("مخصوص غذای خاصی است؟");
            Field(x => x.DiscountedFoods)
                .VisibleIf("info.IsFoodSpecific")
                .AsListbox().Label("غذاهای مورد تخفیف");

            Field(x => x.Start).Label("تاریخ شروع اعتبار");
            Field(x => x.End).Label("تاریخ پایان اعتبار");


            AutoSet(x => x.Shop).Value("info.Shop");

            Button("ذخیره").OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("ذخیره شد.");
                x.ReturnToPreviousPage();
            });
            Button("لغو").OnClick(x =>x.ReturnToPreviousPage());
            ViewModelProperty<Domain.Shop>("Shop")
                .OnBound("info.Shop=Context.Current.User().Extract<ShopUser>().Shop;");

        }
    }
}
