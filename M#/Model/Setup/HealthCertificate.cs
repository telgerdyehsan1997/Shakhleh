using MSharp;

namespace Domain
{
    public class HealthCertificate : EntityType
    {
        public HealthCertificate()
        {
            this.Archivable();
            String("Code").Max(3).MinLength(3).Unique().Mandatory();
            String("Description");
            String("Name").Calculated().Getter(@"Code + ""-"" + Description");
        }

    }
}