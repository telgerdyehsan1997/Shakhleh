using MSharp;

namespace Domain
{
    public class ShopUser : SubType<User>
    {
        public ShopUser()
        {

            Bool("Is admin").Mandatory().Default("false");

            Associate<Shop>("Shop");
        }
    }
}