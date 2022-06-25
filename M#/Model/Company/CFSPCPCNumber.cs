using MSharp;

namespace Domain
{
    internal class CFSPCPCNumber : EntityType
    {

        public CFSPCPCNumber()
        {
            IsEnumReference();
            InstanceAccessors("0610040", "0612071");

            String("Name");
        }
    }
}
