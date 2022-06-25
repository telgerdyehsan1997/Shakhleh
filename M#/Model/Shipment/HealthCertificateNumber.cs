using MSharp;

namespace Domain
{
    internal class HealthCertificateNumber : EntityType
    {
        public HealthCertificateNumber()
        {
            String("Number").Mandatory().Max(26).Mandatory();
            Associate<HealthCertificate>("Health Certificate").Mandatory();
            Associate<Commodity>("Commodity").Mandatory();
        }
    }
}