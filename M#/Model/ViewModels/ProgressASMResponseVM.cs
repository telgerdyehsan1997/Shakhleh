using MSharp;

namespace Domain
{
    class ProgressASMResponseVM : EntityType
    {
        public ProgressASMResponseVM()
        {
            DatabaseMode(DatabaseOption.Transient);
            String("Progress");
            String("ASM message");

        }
    }
}