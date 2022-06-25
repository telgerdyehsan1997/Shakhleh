using MSharp;

namespace Domain
{
    class TransactionType : EntityType
    {
        public TransactionType()
        {
            IsEnumReference();
            InstanceAccessors("Deposit", "Withdrawal", "Pending");

            String("Name");
        }
    }
}