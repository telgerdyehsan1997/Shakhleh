using MSharp;

namespace Domain
{
    class CompanySpecialCPCLink : EntityType
    {
        public CompanySpecialCPCLink()
        {
            Associate<Company>("Company").Mandatory();
            Associate<CPC>("CPC").Mandatory();
            this.Archivable();
        }
    }
}