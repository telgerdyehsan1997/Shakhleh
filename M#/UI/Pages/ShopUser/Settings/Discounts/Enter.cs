using MSharp;
using Domain;

namespace ShopUser.Settings.Discounts
{
    public class EnterPage : SubPage<DiscountsPage>
    {
        public EnterPage()
        {
            Add<Modules.ShopUserDiscountForm>();
        }
    }
}