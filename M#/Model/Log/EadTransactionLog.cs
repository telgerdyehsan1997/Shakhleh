using MSharp;

namespace Domain
{
    class EadTransactionLog : SubType<TransactionLog>
    {
        public EadTransactionLog()
        {
            Associate<Consignment>("Consignment").OnDelete(CascadeAction.CascadeDelete);
        }
    }
}