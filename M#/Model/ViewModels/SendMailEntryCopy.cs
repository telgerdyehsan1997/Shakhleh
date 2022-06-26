using MSharp;

namespace Domain
{
    class SendMailEntryCopyViewModel : EntityType
    {
        public SendMailEntryCopyViewModel()
        {
            DatabaseMode(DatabaseOption.Transient);
            String("EORI number").Mandatory();
            String("Email address").Mandatory();
        }
    }
}