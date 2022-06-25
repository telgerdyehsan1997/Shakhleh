using MSharp;

namespace Domain
{
    class RowQuota : EntityType
    {
        public RowQuota()
        {
            String("Quota Number");
            Bool("Preference").Mandatory();
            //AssociateManyToMany<Country>("Countries");
            String("Countries").Max(int.MaxValue);
            Associate<CommodityCode>("Commodity Code").Mandatory();
        }
    }
}
