using MSharp;

namespace Domain
{
    class ShipmentTransitOffice : EntityType
    {
        public ShipmentTransitOffice()
        {
            var shipment = Associate<Shipment>("Shipment").Mandatory();
            var office = Associate<TransitOffice>("TransitOffice").Mandatory();

            UniqueCombination(new[] { shipment, office });
        }
    }
}
