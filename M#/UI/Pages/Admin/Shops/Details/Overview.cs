using MSharp;
using Domain;
using Admin.Shops;

namespace Admin.Shops
{
    public class OverviewPage : SubPage<ShopsPage>
    {
        public OverviewPage()
        {
            Set(PageSettings.LeftMenu, "AdminShopsMenu");

            Add<Modules.ShopOverview>();
        }
    }
}