using MSharp;

namespace Domain
{
    class ASMFileConsignmentViewModel : EntityType
    {
        public ASMFileConsignmentViewModel()
        {
            DatabaseMode(DatabaseOption.Transient);
            Associate<ShipmentBaseType>("Type");
            String("LRN");
            String("UCR");
        }
    }
}