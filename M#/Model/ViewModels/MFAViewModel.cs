using MSharp;

namespace Domain
{
    class MFAViewModel : EntityType
    {
        public MFAViewModel()
        {
            DatabaseMode(DatabaseOption.Transient);
            String("Access Code");
        }
    }
}