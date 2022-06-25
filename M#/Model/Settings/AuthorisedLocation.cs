using MSharp;

namespace Domain
{
    class AuthorisedLocation : EntityType
    {
        public AuthorisedLocation()
        {
            this.Archivable();

            String("Location name").Mandatory().Unique();
            String("Customs identity").Mandatory().Unique();
            Associate<TransitOffice>("Transit office").Mandatory();
            String("Authorisation number").Mandatory();
            String("Email addresses").Max(null);

            InverseAssociate<GuaranteeLength>("GuaranteeLengths", "AuthorisedLocation");
        }
    }
}