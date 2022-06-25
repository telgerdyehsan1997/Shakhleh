using MSharp;

namespace Domain
{
    class TransitOfficeAlias : EntityType
    {
        public TransitOfficeAlias()
        {
            String("Name").Mandatory().Title("Alias");
            Associate<TransitOffice>("Transit office");
        }
    }
}