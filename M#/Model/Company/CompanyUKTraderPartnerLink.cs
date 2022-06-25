using MSharp;

namespace Domain
{
    class CompanyUKTraderPartnerLink : EntityType
    {
        public CompanyUKTraderPartnerLink()
        {
            Associate<Company>("Company").Mandatory();
            Associate<Company>("UK trader partner").Mandatory();
            Associate<CompanyUKTraderPartnerLinkType>("Type").Mandatory();
            Associate<Consignment>("Consignment").Mandatory();
        }
    }
}