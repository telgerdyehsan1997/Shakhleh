using MSharp;

namespace Domain
{
    class BroadcastMessage : EntityType
    {
        public BroadcastMessage()
        {
            String("Subject").Mandatory();
            BigString("Body", 10).Mandatory();
            SecureFile("Attachment");
            SecureFile("Attachment2").Title("Attachment 2");
            SecureFile("Attachment3").Title("Attachment 3");
            Bool("Has sent").Mandatory();
            Associate<MessageSendToType>("SendTo").Mandatory();
            AssociateManyToMany<CompanyType>("Company types");
            AssociateManyToMany<GVMSType>("GVMS types");
            AssociateManyToMany<SafetyAndSecurity>("Inbound safety and security options");
        }
    }
}