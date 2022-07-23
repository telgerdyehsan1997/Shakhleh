using MSharp;

namespace Domain
{
    public class ShopUser : SubType<User>
    {
        public ShopUser()
        {

            Bool("Is admin").Mandatory(value: false);

            Associate<Shop>("Shop");
        }
    }
}