using MSharp;

namespace Domain
{
    class CompanyShipmentType : EntityType
    {
        public CompanyShipmentType()
        {
            var company = Associate<Company>("Company").Mandatory();
            var shipmentType = Associate<ShipmentType>("ShipmentType").Mandatory();

            UniqueCombination(new[] { company, shipmentType });
        }
    }
}
