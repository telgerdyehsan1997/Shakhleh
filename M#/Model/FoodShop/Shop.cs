using MSharp;

namespace Domain
{
    class Shop : EntityType
    {
        public Shop()
        {
            this.Archivable();

            String("Name");
            BigString("Address");
            String("Description");
            String("Email");
            String("Phone");


            InverseAssociate<ShopUser>("Users", "Shop");
            InverseAssociate<Food>("Foods", "Shop");
            InverseAssociate<Order>("Orders", "Shop");
            InverseAssociate<ShopCustomer>("Customers","Shop");
            InverseAssociate<Discount>("Discounts","Shop");
        }
    }
}
