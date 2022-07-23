using Domain;
using MSharp;

namespace Modules
{
    public class FoodShopView : ViewModule<FoodShop>
    {
        public FoodShopView()
        {
            DataSource("info.Shop");
            HeaderText("مشخصات مغازه");

            Field(x => x.Name).LabelText("نام");
            Field(x => x.Description).LabelText("توضیحات");
            Field(x => x.Address).LabelText("آدرس");
            Field(x => x.Email).LabelText("ایمیل");
            Field(x => x.Phone).LabelText("شماره تلفن");

            Button("بازگشت").OnClick(x =>x.ReturnToPreviousPage());
            ViewModelProperty<Domain.FoodShop>("Shop").FromRequestParam("shop");

        }
    }
}
