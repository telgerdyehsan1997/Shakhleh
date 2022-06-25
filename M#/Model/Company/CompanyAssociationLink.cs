using MSharp;

namespace Domain
{
    //Note: Adding many to many association caused an error on the Company entity.
    //      The bridge table was therefore created manually.
    class CompanyAssociationLink : EntityType
    {
        public CompanyAssociationLink()
        {
            Associate<Company>("Company").DatabaseIndex();
            Associate<Company>("Associated to company").DatabaseIndex();
        }
    }
}