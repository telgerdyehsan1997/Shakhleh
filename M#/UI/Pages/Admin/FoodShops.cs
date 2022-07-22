using MSharp;
using Domain;

namespace Admin
{
    public class FoodShopsPage : SubPage<AdminPage>
    {
        public FoodShopsPage()
        {
            OnStart(x => x.Go<FoodShops.ShopsPage>().RunServerSide());
        }
    }
}