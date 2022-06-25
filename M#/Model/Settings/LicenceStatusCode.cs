using MSharp;

namespace Domain
{
    class LicenceStatusCode : EntityType
    {
        public LicenceStatusCode()
        {
            String("Status Code");
            Associate<ShipmentType>("Type");
            Associate<LicenceType>("Licence Type");
            BigString("Description");
            Bool("IsForShipmentsInAndOutOfUK").Mandatory();

            ToStringExpression("StatusCode");
            this.Archivable();

        }
    }
}