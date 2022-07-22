using Domain;
using MSharp;

namespace Modules
{
    public class FoodShopForm : FormModule<FoodShop>
    {
        public FoodShopForm()
        {
            HeaderText("مغازه");

            Field(x => x.Name).Label("نام");
            Field(x => x.Description).Label("توضیحات");
            Field(x => x.Address).Label("آدرس");
            Field(x => x.Email).Label("ایمیل");
            Field(x => x.Phone).Label("شماره تلفن");

            Button("ذخیره").OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("ذخیره شد.");
                x.ReturnToPreviousPage();
            });
            Button("لغو").OnClick(x =>x.ReturnToPreviousPage());

        }
    }
}
