using MSharp;

namespace Domain
{
    class FoodShop : EntityType
    {
        public FoodShop()
        {
            this.Archivable();

            String("Name");
            BigString("Address");
            String("Description");
            String("Email");
            String("Phone");


            InverseAssociate<FoodShopUser>("Users", "Shop");
            AssociateManyToMany<FoodShopCustomer>("Customers");
        }
    }
}
