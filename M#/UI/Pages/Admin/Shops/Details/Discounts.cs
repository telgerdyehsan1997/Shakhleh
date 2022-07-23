using MSharp;
using Domain;

namespace Admin.Shops
{
    public class DiscountsPage : SubPage<ShopsPage>
    {
        public DiscountsPage()
        {
            Set(PageSettings.LeftMenu, "AdminShopsMenu");

            Add<Modules.DiscountList>();
        }
    }
}