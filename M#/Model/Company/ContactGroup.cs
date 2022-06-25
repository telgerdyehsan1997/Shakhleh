using MSharp;

namespace Domain
{
    class ContactGroup : EntityType
    {
        public ContactGroup()
        {
            String("Group name").Mandatory();
            Associate<Company>("Company").DatabaseIndex();
            AssociateManyToMany<Person>("Contacts");

            this.Archivable();
        }
    }
}
