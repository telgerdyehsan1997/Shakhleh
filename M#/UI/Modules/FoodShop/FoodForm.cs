using Domain;
using MSharp;

namespace Modules
{
    public class FoodForm : FormModule<Food>
    {
        public FoodForm()
        {
            HeaderText("غذا");

            Field(x => x.Name).Label("نام");
            Field(x => x.Description).Label("توضیحات");
            Field(x => x.Price).Label("قیمت");
            Field(x => x.Image).Label("تصویر");

            AutoSet(x => x.Shop).Value("info.Shop");

            Button("ذخیره").OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("ذخیره شد.");
                x.ReturnToPreviousPage();
            });
            Button("لغو").OnClick(x =>x.ReturnToPreviousPage());
            ViewModelProperty<Domain.Shop>("Shop").FromRequestParam("shop");

        }
    }
}
