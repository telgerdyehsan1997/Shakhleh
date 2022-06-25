using MSharp;

namespace Domain
{
    class VATType : EntityType
    {
        public VATType()
        {
            LogEvents(false);
            String("Name").Mandatory().Unique();
        }
    }
}