using MSharp;

namespace Domain
{
    class TransitOffice : EntityType
    {
        public TransitOffice()
        {
            this.SoftDelete();

            this.Archivable();
            Name("Offices of Transit");
            String("Country name").Mandatory();
            String("Country code").Mandatory().Max(2);
            String("NCTS Code").Mandatory().MinLength(8).Max(8).Unique();
            String("Usual name").Mandatory();
            Bool("Departure").Mandatory(value: false);
            Bool("Destination").Mandatory(value: false);
            Bool("Transit").Mandatory(value: false);
            String("Nearest office");
            String("Geo location");

            String("DisplayValue").CalculatedFrom(@"$""{CountryCode} {UsualName} {NCTSCode} {CountryName}""");

            InverseAssociate<TransitOfficeAlias>("TransitOfficeAlias", "TransitOffice");
            InverseAssociate<AuthorisedLocation>("AuthorisedLocation", "TransitOffice");

            ToStringExpression(@"$""{CountryCode} {UsualName} {NCTSCode} {CountryName}""");
        }
    }
}