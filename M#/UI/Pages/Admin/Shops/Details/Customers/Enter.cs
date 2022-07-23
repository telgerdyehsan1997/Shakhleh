using MSharp;
using Domain;

namespace Admin.Shops.Details.Customers
{
    public class EnterPage : SubPage<CustomersPage>
    {
        public EnterPage()
        {
            Add<Modules.CustomerForm>();
        }
    }
}