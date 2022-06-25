using MSharp;

namespace Domain
{
    class ShipmentApiTransactionLog : SubType<TransactionLog>
    {
        public ShipmentApiTransactionLog()
        {
           // Associate<Shipment>("Shipment").OnDelete(CascadeAction.CascadeDelete);
        }
    }
}