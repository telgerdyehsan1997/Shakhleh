using MSharp;

namespace Domain
{
    class CompanyUKTraderPartnerLinkType : EntityType
    {
        public CompanyUKTraderPartnerLinkType()
        {           
            InstanceAccessors("UKTrader", "Partner");
            String("Name").Mandatory();
            String("Display");
        }
    }
}