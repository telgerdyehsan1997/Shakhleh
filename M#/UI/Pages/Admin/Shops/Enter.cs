using MSharp;
using Domain;

namespace Admin.Shops
{
    public class EnterPage : SubPage<ShopsPage>
    {
        public EnterPage()
        {
            Add<Modules.ShopForm>();
        }
    }
}