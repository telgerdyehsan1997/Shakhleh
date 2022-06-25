using MSharp;

namespace Domain
{
    class GuaranteeLength : EntityType
    {
        public GuaranteeLength()
        {
            Associate<AuthorisedLocation>("AuthorisedLocation").Mandatory();
            Int("Length").Mandatory();
            ToStringExpression("Length.ToString()");
            this.Archivable();
        }
    }
}