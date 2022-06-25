using MSharp;

namespace Domain
{
    class Route : EntityType
    {
        public Route()
        {
            this.Archivable();


            Associate<Port>("UK port").Mandatory();
            Associate<Port>("Non-UK port").Mandatory();
            Bool("IsManual").Mandatory(value: false);
            UniqueCombination(new[] { "UK port", "Non-UK port" });

            InverseAssociate<RouteItinerary>("RouteItinerary", "Route");
            InverseAssociate<RouteItineraryCountry>("RouteItineraryCountry", "Route");
            InverseAssociate<Shipment>("Shipment", "Route");

            ToStringExpression("UKPort.PortName + \" to \"  + Non_UKPort.PortName");
        }
    }
}