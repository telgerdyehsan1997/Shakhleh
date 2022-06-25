using MSharp;

namespace Domain
{
    class SecondQuantityDescription : EntityType
    {
        public SecondQuantityDescription()
        {
            DefaultToString = String("Quantity code").Mandatory().Unique();
            String("Description").Mandatory();
            this.Archivable();
        }
    }
}