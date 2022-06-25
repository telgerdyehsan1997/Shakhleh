using MSharp;

namespace Domain
{
    class ImportType : EntityType
    {
        public ImportType()
        {
            InstanceAccessors("CommodityCode", "Company", "Product", "AuthorisedLocation", "Commodity", "Itinerary", "UnCodes", "RowQuota")
              .IsEnumReference()
              .LogEvents(false);

            String("Name").Mandatory();
        }
    }
}