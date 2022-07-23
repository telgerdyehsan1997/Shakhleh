using MSharp;

namespace Domain
{
    public class ShopCustomer : SubType<User>
    {
        public ShopCustomer()
        {

            Associate<Shop>("Shop").Mandatory();
        }
    }
}