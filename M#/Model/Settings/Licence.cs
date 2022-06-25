using MSharp;

namespace Domain
{
    class Licence : EntityType
    {
        public Licence()
        {
            Associate<ShipmentType>("Type").Mandatory();
            String("Licence Name").Mandatory();
            Associate<LicenceType>("Licence Type").Mandatory();
            String("Licence Identifier").Mandatory();
            String("Chief licence code");
            Associate<LicenceStatusCode>("Licence status code");
            Bool("Quantity").Mandatory(value: false);
            Bool("RPTID").Mandatory(value: false);

            this.Archivable();

            ToStringExpression("LicenceName");
        }
    }
}