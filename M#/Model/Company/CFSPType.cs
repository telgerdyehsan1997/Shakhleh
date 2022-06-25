using MSharp;

namespace Domain
{
    internal class CFSPType : EntityType
    {

        public CFSPType()
        {
            IsEnumReference();
            InstanceAccessors("Own", "Channelports", "None");

            String("Name");
        }
    }
}
