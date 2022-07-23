using Domain;
using MSharp;

namespace Modules
{
    public class ShopUserUserForm : FormModule<Domain.ShopUser>
    {
        public ShopUserUserForm()
        {
            HeaderText("کاربر");

            Field(x => x.FirstName).Label("نام");
            Field(x => x.LastName).Label("نام خانوادگی");
            Field(x => x.Email).Label("ایمیل");
            Field(x => x.Phone).Label("شماره تلفن");
            Field(x => x.IsAdmin).Label("ادمین");

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
