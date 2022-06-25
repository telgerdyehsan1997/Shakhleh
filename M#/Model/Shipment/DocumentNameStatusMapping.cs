using MSharp;

namespace Domain
{
    class DocumentNameStatusMapping : EntityType
    {
        public DocumentNameStatusMapping()
        {
            String("Docuemet Name").Mandatory().Unique();
            String("XML Status").Mandatory().Unique();
            Associate<Progress>("Status").Mandatory();
        }
    }
}